using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;
using System.Security.Claims;

namespace MyShopAPI.Helpers
{
    public static class CartManager
    {
        public async static Task ClearCart(this IUnitOfWork _unitOfWork, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Name);

            var customer = await _unitOfWork.Customers.Get(user => user.Email == email);

            var cartItems = await _unitOfWork.Carts.GetAll(cart => cart.CustomerId == customer.Id && cart.Quantity != 0);

            var updatedItems = new List<Cart>();

            foreach (var item in cartItems)
            {
                var cart = new Cart
                {
                    Id = item.Id,
                    CustomerId = customer.Id,
                    ProductId = item.ProductId,
                    Quantity = 0,
                    IsPurchased = 1
                };
                updatedItems.Add(cart);
            }

            _unitOfWork.Carts.UpdateRange(updatedItems);

            await _unitOfWork.Save();
        }
    }
}
