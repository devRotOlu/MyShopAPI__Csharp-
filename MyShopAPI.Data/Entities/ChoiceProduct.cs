using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShopAPI.Data.Entities
{
    //[PrimaryKey(nameof(CustomerId), nameof(ProductId))]
    public class ChoiceProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, ForeignKey("Customer")]
        public string CustomerId { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
