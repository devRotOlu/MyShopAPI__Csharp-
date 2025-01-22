using MyShopAPI.Core.EntityDTO.ProoductDTO;

namespace MyShopAPI.Core.EntityDTO.WishlistDTO
{
    public class GetWishlistDTO
    {
        public int Id;
        public GetProductDTO Product { get; set; } = null!;
    }
}
