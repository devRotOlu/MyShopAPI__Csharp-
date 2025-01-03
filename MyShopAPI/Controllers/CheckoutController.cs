using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.AuthManager;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Services.PayPal;

namespace MyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthManager _authManager;
        private readonly IPayPalService _payPalService;

        public CheckoutController(IUnitOfWork unitOfWork, IAuthManager authManager, IPayPalService payPalService)
        {
            _unitOfWork = unitOfWork;
            _authManager = authManager;
            _payPalService = payPalService;
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder([FromQuery] string customerId)
        {
            var user = await _authManager.GetUserByIdAsync(customerId);

            if (user == null) return BadRequest();

            var items = await _unitOfWork.CartsAndWishlists.GetAll(item => item.CustomerId == customerId && item.Quantity != 0, include: item => item.Include(item => item.Product));

            if (items == null || items.Count() == 0) return BadRequest();

            var orderId = await _payPalService.CreateOrder(items.ToList());

            return Ok();
        }

    }
}
