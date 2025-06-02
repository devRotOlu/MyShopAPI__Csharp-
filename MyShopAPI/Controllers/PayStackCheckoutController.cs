using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShopAPI.Core.AuthManager;
using MyShopAPI.Core.DTOs.PaystackDTOs;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Helpers;
using MyShopAPI.Services.PayStack;

namespace MyShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PayStackCheckoutController : ControllerBase
    {
        private readonly IPayStackService _payStackService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthManager _authManager;

        public PayStackCheckoutController(IPayStackService payStackService, IUnitOfWork unitOfWork, IAuthManager authManager)
        {
            _payStackService = payStackService;
            _unitOfWork = unitOfWork;
            _authManager = authManager;
        }

        [HttpGet("initialize")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
        public async Task<IActionResult> InitializeTransaction([FromQuery] string email)
        {
            var user = await _authManager.GetUserByEmailAsync(email);

            if (user == null)
            {
                return BadRequest();
            }

            var totalCost = await _unitOfWork.ComputeCartTotal(user.Id);

            var response = _payStackService.InitializePayment(email, totalCost);

            return Ok(new { authorizationURL = response.Data.AuthorizationUrl,reference= response.Data.Reference });
        }

        [HttpPost("verify_payment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status402PaymentRequired)]
        public async Task<IActionResult> VerifyTransaction(PaymentVerificationDTO paymentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _payStackService.VerifyPayment(paymentDTO.Reference);

            var orderId = await _unitOfWork.AddToOrderAndClearCart(User,paymentDTO.ProfileId,paymentDTO.OrderInstruction);

            return Ok(new {orderId});
        }
    }
}
