using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;
using System.Security.Claims;

namespace MyShopAPI.Helpers
{
    public static class OrderManager
    {
        public async static Task AddToOrder(this IUnitOfWork _unitOfWork, ClaimsPrincipal user,int profileId)
        {
            var email = user.FindFirstValue(ClaimTypes.Name);

            var customer = await _unitOfWork.Customers.Get(user => user.Email == email);

            var items = await _unitOfWork.Carts.GetAll(item=>item.CustomerId == customer.Id);

            var order = new CustomerOrder();

            order.CustomerId = customer.Id;

            order.ItemsOrdered = items;

            order.DeliveryProfileId = profileId;

            order.OrderStatus = "Processing";

            await _unitOfWork.Orders.Insert(order);

            await _unitOfWork.Save();
        }
    }
}
