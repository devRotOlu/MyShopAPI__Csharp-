using MyShopAPI.Data.Entities;

namespace MyShopAPI.Core.DTOs.OrderDTOs
{
    public class GetOrderDTO
    {
        public int Id { get; set; }

        public string OrderId { get; set; } = null!;

        public string OrderDate { get; set; } = null!;

        public DeliveryProfile DeliveryProfile { get; set; } = null!;

        public string OrderStatus { get; set; } = null!;

        public IEnumerable<CartOrderDTO> OrderedItems { get; set; } = null!;
    }
}
