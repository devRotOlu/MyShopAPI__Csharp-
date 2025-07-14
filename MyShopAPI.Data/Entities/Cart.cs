using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Data.Entities
{
    public class Cart : ChoiceProduct
    {
        [Required]
        public int Quantity { get; set; }

        public int IsPurchased { get; set; }

        //[Column(TypeName = "decimal(18,4)")]
        [Precision(18, 4)]
        public decimal TotalCost { get; set; }

        public ICollection<CartOrder> Orders { get; set; } = null!;

        public DateTime? DeletedAt { get; set; }

        public DateTime AddedAt { get; set; }
    }
}
