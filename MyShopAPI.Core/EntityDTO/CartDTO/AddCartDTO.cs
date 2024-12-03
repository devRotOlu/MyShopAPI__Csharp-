using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.EntityDTO.CartDTO
{
    public class AddCartDTO
    {
        [Required,MinLength(1)]
        public string CustomerId { get; set; } = null!;
        [Required, Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
