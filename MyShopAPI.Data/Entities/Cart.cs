using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShopAPI.Data.Entities
{
    public class Cart:ChoiceProduct
    {
        [Required]
        public int Quantity { get; set; }

        public int IsPurchased { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalCost { get; set; }

        public ICollection<CartOrder> Orders { get; set; } = null!;
    }
}
