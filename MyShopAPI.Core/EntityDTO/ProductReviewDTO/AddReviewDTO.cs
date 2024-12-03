using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.EntityDTO.ProductReviewDTO
{
    public class AddReviewDTO:ReviewDTO
    {
        [Required]
        public string CustomerId { get; set; } = null!;
        [Required]
        public int ProductId { get; set; }
    }
}
