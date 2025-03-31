using MyShopAPI.Core.EntityDTO.ProoductDTO;

namespace MyShopAPI.Core.EntityDTO.CartDTO
{
    public class GetCartDTO
    {
        public int Id { get; set; }
        public BaseGetProductDTO Product { get; set; } = null!;
        public int CartQuantity { get; set; }
    }
}
