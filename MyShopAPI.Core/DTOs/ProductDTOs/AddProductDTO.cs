using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs.ProductDTOs
{
    public class AddProductDTO : ProductDTO
    {
        [Required]
        public ICollection<IFormFile> Photos { get; set; } = null!;
        [Required]
        public int CategoryId { get; set; }
        /// <summary>
        /// Json string representing a collection of
        /// ProductAttributeDTO's
        /// </summary>
        public string? AttributesJson { get; set; }
    }
}
