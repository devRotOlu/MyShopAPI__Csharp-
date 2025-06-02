using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShopAPI.Data.Entities
{
    [PrimaryKey(nameof(AttributeId), nameof(ProductId))]
    public class ProductAttribute
    {
        [ForeignKey("Attribute")]
        public int AttributeId { get; set; }
        public Attribute Attribute { get; set; } = null!;

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [MinLength(1)]
        public string Value { get; set; } = null!;
    }
}
