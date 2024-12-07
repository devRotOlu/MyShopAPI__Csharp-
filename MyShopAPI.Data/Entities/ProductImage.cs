using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShopAPI.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        [Required]
        public string Url { get; set; } = null!;
        [Required]
        public string PublicId { get; set; } = null!;
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
