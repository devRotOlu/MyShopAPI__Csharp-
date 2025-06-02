using MyShopAPI.Core.DTOs.ProductDTOs;
using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs.ProductAttributeDTOs
{
    public class ProductAttributeDTO
    {
        [Required, Range(1, double.MaxValue)]
        public int AttributeId { get; set; }
        [Required, MinLength(1)]
        public string Value { get; set; } = null!;
        public AttributeDTO? Attribute { get; set; } = null!;
    }
}
