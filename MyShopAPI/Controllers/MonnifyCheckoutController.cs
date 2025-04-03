using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.AuthManager;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Helpers;
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
        public async Task<IActionResult> InitializeTransaction(string customerEmail)
        {

            var user = await _authManager.GetUserByEmailAsync(customerEmail);

            if (user == null)
            {
                return BadRequest();
            }

            await _monnifyService.Authorization();

            var items = await _unitOfWork.Carts.GetAll(item => item.CustomerId == user.Id && item.Quantity != 0, include: item => item.Include(item => item.Product));

            var result = await _monnifyService.InitilaizeTransaction(items.ToList(), customerEmail);

            if (!result.RequestSuccessful)
            {
                return StatusCode(500);
            }

            return Ok(new { transactionReference = result.ResponseBody.TransactionReference });
        }

        [HttpGet("bank_transfer")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBankTransferInfo([FromQuery] string bankCode, [FromQuery] string transactionReference)
        {
            if (string.IsNullOrEmpty(bankCode) || string.IsNullOrEmpty(transactionReference))
            {
                return BadRequest();
            }

            await _monnifyService.Authorization();

            var result = await _monnifyService.GetBankTransferInfo(bankCode, transactionReference);

            if (!result.RequestSuccessful)
            {
                return StatusCode(402,new {message = result.ResponseMessage});
            }

            return Ok(result);
        }

        [HttpPost("card_charge")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CardPayment([FromBody] ChargeCardRequest chargeCard, [FromQuery] int profileId)
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


            if (!result.RequestSuccessful)
            {
                return StatusCode(402, new { message = result.ResponseMessage });
            }

            await _unitOfWork.AddToOrder(User,profileId);

            await _unitOfWork.ClearCart(User);

            return Ok();
        }

        [HttpGet("transaction_status")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionStatus([FromQuery] string transactionRef, [FromQuery] int profileId)
        {
            if (string.IsNullOrEmpty(transactionRef) || profileId < 0)
            {
                return BadRequest();
            }

            await _monnifyService.Authorization();

            var result = await _monnifyService.GetTransactionStatus(transactionRef);

            var paidStatus = "PAID";
            var overPaidStatus = "OVERPAID";

            var status = result.ResponseBody.PaymentStatus;

            if (status != paidStatus && status != overPaidStatus)
            {
                return StatusCode(402);
            }
            else if (!result.RequestSuccessful)
            {
                return StatusCode(500);
            }

            await _unitOfWork.AddToOrder(User, profileId);

            await _unitOfWork.ClearCart(User);

            return Ok();
        }
    }
}
