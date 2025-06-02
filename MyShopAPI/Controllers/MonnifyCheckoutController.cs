using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShopAPI.Core.AuthManager;
using MyShopAPI.Core.DTOs.MonnifyDTOs;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Helpers;
using MyShopAPI.Services.Monnify;

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

        public MonnifyCheckoutController(IMonnifyService monnifyService, IAuthManager authManager, IUnitOfWork unitOfWork)
        {
            _monnifyService = monnifyService;
            _authManager = authManager;
            _unitOfWork = unitOfWork;
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

            var totalCost = await _unitOfWork.ComputeCartTotal(user.Id);

            var result = await _monnifyService.InitilaizeTransaction(customerEmail, (float)totalCost);

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
                return StatusCode(402, new { message = result.ResponseMessage });
            }

            return Ok(result);
        }

        [HttpPost("card_charge")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CardPayment([FromBody] CardPaymentDTO cardDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _monnifyService.Authorization();

            var result = await _monnifyService.CardPayment(cardDTO.CardDetails);


            if (!result.RequestSuccessful)
            {
                return StatusCode(402, new { message = result.ResponseMessage });
            }

            var orderId = await _unitOfWork.AddToOrderAndClearCart(User, cardDTO.profileId, cardDTO.OrderInstruction);

            return Ok(new { orderId });
        }

        [HttpPost("transaction_status")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionStatus(TransactionStatusDTO statusDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _monnifyService.Authorization();

            var result = await _monnifyService.GetTransactionStatus(statusDTO.TransactionReference);

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

            var orderId = await _unitOfWork.AddToOrderAndClearCart(User, statusDTO.ProfileId, statusDTO.OrderInstruction);

            return Ok(new { orderId });
        }
    }
}
