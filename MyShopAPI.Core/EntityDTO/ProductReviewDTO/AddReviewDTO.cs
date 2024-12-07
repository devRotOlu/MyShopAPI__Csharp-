using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.EntityDTO.ProductReviewDTO
{
    public class AddReviewDTO:ProductReviewDTO
    {
        [Required]
        public string ReviewerId { get; set; } = null!;
        [Required]
        public int ProductId { get; set; }
    }
}
