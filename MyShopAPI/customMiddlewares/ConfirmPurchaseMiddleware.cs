using AutoMapper;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;
using System.Security.Claims;

namespace MyShopAPI.customMiddlewares
{
    public class ConfirmPurchaseMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly List<string> paths;

        public ConfirmPurchaseMiddleware(RequestDelegate next, IMapper mapper)
        {
            _next = next;

            paths = new List<string>
            {
                "/api/MonnifyCheckout/card_charge",
                "/api/MonnifyCheckout/transaction_status",
                "/api/PayPalCheckout/capture_order"
            };
            _mapper = mapper;
        }

        public async Task Invoke(HttpContext context)
        {

            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status200OK && paths.Contains(context.Request.Path))
            {
                using (var scope = context.RequestServices.CreateScope())
                {
                    var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var email = context.User.FindFirstValue(ClaimTypes.Name);

                    var user = await _unitOfWork.Customers.Get(user => user.Email == email);

                    var cartItems = await _unitOfWork.Carts.GetAll(cart => cart.CustomerId == user.Id && cart.Quantity != 0);

                    var updatedItems = new List<Cart>();

                    foreach (var item in cartItems)
                    {
                        var cart = new Cart
                        {
                            Id = item.Id,
                            CustomerId = user.Id,
                            ProductId = item.ProductId,
                            Quantity = 0,
                        };

                        cart.IsPurchased = 1;
                    }

                    _unitOfWork.Carts.UpdateRange(updatedItems);

                }
            }
        }
    }
}