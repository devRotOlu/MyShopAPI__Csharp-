using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.IRepository;

namespace MyShopAPI.Helpers
{
    public static class CartManager
    {
        public async static Task<decimal> ComputeCartTotal(this IUnitOfWork _unitOfWork,string userId)
        {
            var cartItems = await _unitOfWork.Carts.GetAll(cart=> cart.CustomerId == userId)
                                            .ToListAsync();

            decimal totalCost = 0;

            foreach (var item in cartItems)
            {
                totalCost += item.TotalCost;
            }

            return Math.Round(totalCost,2);
        }
    }
}
