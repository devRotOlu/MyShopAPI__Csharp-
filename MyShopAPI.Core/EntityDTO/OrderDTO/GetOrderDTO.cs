using MyShopAPI.Core.EntityDTO.CartDTO;
using MyShopAPI.Data.Entities;

namespace MyShopAPI.Core.EntityDTO.OrderDTO
{
    public class GetOrderDTO
    {
        public string OrderId { get; set; } = null!;

        public DateOnly OrderDate { get; set; } 

        public DeliveryProfile DeliveryProfile { get; set; } = null!;

        public string OrderStatus { get; set; } = null!;

        public IEnumerable<GetCartDTO> ItemsOrdered { get; set; } = null!;
    }
}
