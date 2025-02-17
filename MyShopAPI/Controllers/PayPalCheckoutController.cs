using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.AuthManager;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Services.PayPal;
using PaypalServerSdk.Standard.Models;

namespace MyShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalCheckoutController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthManager _authManager;
        private readonly IPayPalService _payPalService;

        public PayPalCheckoutController(IUnitOfWork unitOfWork, IAuthManager authManager, IPayPalService payPalService)
        {
            _unitOfWork = unitOfWork;
            _authManager = authManager;
            _payPalService = payPalService;
        }

        [HttpPost("create_order")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateOrder([FromQuery] string customerId)
        {
            var user = await _authManager.GetUserByIdAsync(customerId);

            if (user == null) return BadRequest();

            var items = await _unitOfWork.Carts.GetAll(item => item.CustomerId == customerId && item.Quantity != 0, include: item => item.Include(item => item.Product));

            if (items == null || items.Count() == 0) return BadRequest();

            var response = await _payPalService.CreateOrder(items.ToList());
            var orderId = response.Data.Id;
            return Ok(orderId);
        }

        [HttpPost("capture_order")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CaptureOrder([FromBody] OrdersCaptureInput input)
        {
            var result = await _payPalService.CaptureOrder(input);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("track_order")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrder([FromQuery] string orderId)
        {
            //var user = await _authManager.GetUserByIdAsync(customerId);

            //if (user == null) return BadRequest();

            var result = await _payPalService.GetOrder(orderId);

            return Ok(result.Data);
        }
    }
}
