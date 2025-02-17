using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Data.Entities
{
    public class Cart:ChoiceProduct
    {
        [Required]
        public int Quantity { get; set; }

        public int IsPurchased { get; set; }
    }
}
