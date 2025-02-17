using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyShopAPI.Data.Entities
{
    [PrimaryKey(nameof(CustomerId), nameof(ProductId))]
    public class ChoiceProduct
    {
        public int Id { get; set; }
        [Required, ForeignKey("Customer")]
        public string CustomerId { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
