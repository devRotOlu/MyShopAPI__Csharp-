using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.EntityDTO.ProductReviewDTO
{
    public class ProductReviewDTO
    {
        [Required]
        public string Review { get; set; } = null!;
        [Required, Column(TypeName = "decimal(3,2)"),Range(1,5)]
        public decimal Rating { get; set; }
    }
}
