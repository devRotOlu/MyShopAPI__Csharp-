using MyShopAPI.Core.DTOs.ProductDTOs;

namespace MyShopAPI.Core.EntityDTO.WishlistDTOs
{
    public class GetWishlistDTO
    {
        public int Id;
        public BaseGetProductDTO Product { get; set; } = null!;
    }
}
