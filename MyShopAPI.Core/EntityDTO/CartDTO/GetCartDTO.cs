using MyShopAPI.Core.EntityDTO.ProoductDTO;

namespace MyShopAPI.Core.EntityDTO.CartDTO
{
    public class GetCartDTO
    {
        public GetProductDTO Product { get; set; } = null!;
        public int CartQuantity { get; set; }
    }
}
