using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.EntityDTO
{
    public class OrderDTO
    {
        [Required]
        public string CustomerId { get; set; } = null!;
        [Required]
        public int DeliveryProfileId { get; set; }
    }
}
