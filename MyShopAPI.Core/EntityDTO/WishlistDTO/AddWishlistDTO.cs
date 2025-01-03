using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.EntityDTO.WishlistDTO
{
    public class AddWishlistDTO
    {
        [Required, MinLength(1)]
        public string CustomerId { get; set; } = null!;
        [Required, Range(1, int.MaxValue)]
        public int ProductId { get; set; }
    }
}
