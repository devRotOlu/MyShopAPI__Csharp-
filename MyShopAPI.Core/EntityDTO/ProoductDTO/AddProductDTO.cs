using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.EntityDTO.ProoductDTO
{
    public class AddProductDTO : ProductDTO
    {
        [Required]
        public ICollection<IFormFile> Photos { get; set; } = null!;
    }
}
