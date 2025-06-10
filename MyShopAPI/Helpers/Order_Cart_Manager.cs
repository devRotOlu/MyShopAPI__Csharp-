using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;
using System.Security.Claims;

namespace MyShopAPI.Helpers
{
    public static class Order_Cart_Manager
    {
        public async static Task<string> AddToOrderAndClearCart(this IUnitOfWork _unitOfWork, ClaimsPrincipal user,int profileId,string? orderInstruction)
        {
            var email = user.FindFirstValue(ClaimTypes.Name);

            var customer = await _unitOfWork.Customers.Get(user => user.Email == email);

            var items = await _unitOfWork.Carts.GetAll(item=>item.CustomerId == customer.Id && item.Quantity != 0).ToListAsync();

            var order = new CustomerOrder();

            order.CustomerId = customer.Id;

            order.DeliveryProfileId = profileId;

            var updatedItems = new List<Cart>();

            foreach (var item in items)
            {
                item.Orders = new List<CartOrder>();

                var cartOrder = new CartOrder
                {
                    CustomerOrder = order,
                    CartItem = item,
                    OrderedQuantity = item.Quantity,
                    OrderInstruction = orderInstruction
                };
                item.Orders.Add(cartOrder);
                item.Quantity = 0; // set each cart item quantiy to zero i.e., clearing cart.
                item.DeletedAt = DateTime.Now;
                item.IsPurchased = 1;
                updatedItems.Add(item);
            }

            _unitOfWork.Carts.UpdateRange(updatedItems);
            await _unitOfWork.Save();

            return order.OrderId;
        }
    }
}
