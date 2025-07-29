using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyShopAPI.Data.Entities
{
    //[PrimaryKey(nameof(ReviewerId), nameof(ProductId),nameof(OrderId))]
    public class ProductReview
    {
        [Required, ForeignKey(" Reviewer")]
        public string ReviewerId { get; set; } = null!;
        public Customer Reviewer { get; set; } = null!;
        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        [Required, ForeignKey("Order")]
        public int OrderId { get; set; }
        public CustomerOrder Order { get; set; } = null!;
        [Required]
        public string Review { get; set; } = null!;
        [Required, Precision(3, 2)]
        public decimal Rating { get; set; }
        public DateTime ReviewDate { get; set; } 
    }
}
