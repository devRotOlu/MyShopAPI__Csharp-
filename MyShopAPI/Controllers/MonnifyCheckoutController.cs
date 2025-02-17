using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.AuthManager;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Services.Models.Monnify.ChargeCard;
using MyShopAPI.Services.Monnify;
using MyShopAPI.Services.RSA;

namespace MyShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MonnifyCheckoutController : ControllerBase
    {
        private readonly IMonnifyService _monnifyService;
        private readonly IAuthManager _authManager;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IDataProtector _protector;
        private readonly IRSAService _raService;

        public MonnifyCheckoutController(IMonnifyService monnifyService, IAuthManager authManager, IUnitOfWork unitOfWork, IRSAService rSAService)
        {
            _monnifyService = monnifyService;
            _authManager = authManager;
            _unitOfWork = unitOfWork;
            _raService = rSAService;
            //_protector = provider.CreateProtector("MyPurpose");
        }

        [HttpGet("initialize")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> InitializeTransaction(string customerEmail)
        {

            var user = await _authManager.GetUserByEmailAsync(customerEmail);

            if (user == null)
            {
                return BadRequest();
            }

            await _monnifyService.Authorization();

            var items = await _unitOfWork.Carts.GetAll(item => item.CustomerId == user.Id && item.Quantity != 0, include: item => item.Include(item => item.Product));

            var transactionReference = await _monnifyService.InitilaizeTransaction(items.ToList(), customerEmail);

            return Ok(new { transactionReference });
        }

        [HttpGet("bank_transfer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBankTransferInfo([FromQuery] string bankCode, [FromQuery] string transactionReference)
        {
            if (string.IsNullOrEmpty(bankCode) || string.IsNullOrEmpty(transactionReference))
            {
                return BadRequest();
            }

            await _monnifyService.Authorization();
            var result = await _monnifyService.GetBankTransferInfo(bankCode, transactionReference);
            return Ok(result);
        }

        [HttpPost("card_charge")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CardPayment([FromBody] ChargeCardRequest chargeCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //FromBase64Transform fromBase64Transform = new FromBase64Transform();
            //var cipherByte = Convert.FromBase64String(body);

            //var decrypted = _raService.Decrypt(cipherByte);

            await _monnifyService.Authorization();

            var result = await _monnifyService.CardPayment(chargeCard);

            return Ok();
        }

        [HttpGet("transaction_status")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionStatus([FromQuery] string transactionRef)
        {
            if (string.IsNullOrEmpty(transactionRef))
            {
                return BadRequest();
            }

            await _monnifyService.Authorization();

            var result = await _monnifyService.GetTransactionStatus(transactionRef);

            return Ok(result);
        }
    }
}
