using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs.ProductDTOs
{
    public class BaseGetProductDTO:ProductDTO
    {
        [Required]
        public int Id { get; set; }
        public ICollection<ImageDTO> Images { get; set; } = null!;
    }
}
