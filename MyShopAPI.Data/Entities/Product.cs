using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShopAPI.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        //[Required, Column(TypeName = "decimal(15,4)")]
        [Precision(15, 4)]
        public decimal UnitPrice {  get; set; } 
        [Required]
        public uint Quantity { get; set; }
        [Required,ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        [Required]
        public ICollection<ProductImage> Images { get; set; } = null!;
        [Required]
        public ICollection<ProductReview> Reviews { get; set; } = null!;
        //[Column(TypeName = "decimal(3,2)")]
        [Precision(13, 2)]
        public decimal AverageRating { get; set; }
        public IEnumerable<ProductAttribute>? Attributes { get; set; } 
    }
}
