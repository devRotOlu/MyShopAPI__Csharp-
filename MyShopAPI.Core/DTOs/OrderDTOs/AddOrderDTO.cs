using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        [Required]
        public string CustomerId { get; set; } = null!;
        [Required]
        public int DeliveryProfileId { get; set; }
    }
}
