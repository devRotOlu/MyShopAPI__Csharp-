using MyShopAPI.Core.DTOs.ProductDTOs;

namespace MyShopAPI.Core.DTOs.CartDTOs
{
    public class GetCartDTO
    {
        public int Id { get; set; }
        public BaseGetProductDTO Product { get; set; } = null!;
        public int CartQuantity { get; set; }
    }
}
