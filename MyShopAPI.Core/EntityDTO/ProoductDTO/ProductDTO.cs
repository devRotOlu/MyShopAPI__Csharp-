using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.EntityDTO.ProoductDTO
{
    public class ProductDTO
    {
        [Required, StringLength(100,MinimumLength =1)]
        public string Name { get; set; } = null!;
        [StringLength(300, MinimumLength = 1)]
        public string Description { get; set; } = null!;
        [Required, Column(TypeName = "decimal(15,4)")]
        public decimal UnitPrice { get; set; }
        [Required,Range(1,10000)]
        public virtual uint Quantity { get; set; }
    }
}
