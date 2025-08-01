using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs.ProductReviewDTOs
{
    public class AddReviewDTO:ProductReviewDTO
    {
        [Required]
        public string ReviewerId { get; set; } = null!;
        [Required]
        public int ProductId { get; set; }
        //[Required]
        //public int OrderId { get; set; }
    }
}
