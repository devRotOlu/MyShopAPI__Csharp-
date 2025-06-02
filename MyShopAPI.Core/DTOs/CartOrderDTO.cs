using MyShopAPI.Core.DTOs.CartDTOs;
        
namespace MyShopAPI.Core.DTOs
{
    public class CartOrderDTO
    {
        public GetCartDTO CartItem { get; set; } = null!;
        public int OrderedQuantity { get; set; }
    }
}
