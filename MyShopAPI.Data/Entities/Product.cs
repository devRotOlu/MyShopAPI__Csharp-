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
        [Required, Column(TypeName = "decimal(15,4)")]
        public decimal UnitPrice {  get; set; } 
        [Required]
        public uint Quantity { get; set; }
        [Required]
        public ICollection<Image> Images { get; set; } = null!;
        [Column(TypeName = "decimal(3,2)")]
        public decimal AverageRating { get; set; }
    }
}
