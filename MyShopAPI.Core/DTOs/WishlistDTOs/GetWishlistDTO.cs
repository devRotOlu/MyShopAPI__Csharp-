using MyShopAPI.Core.DTOs.ProductDTOs;

namespace MyShopAPI.Core.EntityDTO.WishlistDTOs
{
    public class GetWishlistDTO
    {
        public int Id;
        public GetProductDTO Product { get; set; } = null!;
    }
}
