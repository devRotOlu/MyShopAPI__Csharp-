using MyShopAPI.Core.DTOs.CategoryDTOs;
using MyShopAPI.Core.DTOs.ProductAttributeDTOs;
using MyShopAPI.Core.DTOs.ProductReviewDTOs;

namespace MyShopAPI.Core.DTOs.ProductDTOs
{
    public class GetProductDTO : BaseGetProductDTO
    {
        public ICollection<ReviewDTO> Reviews { get; set; } = null!;
        public decimal AverageRating { get; set; }
        public IEnumerable<ProductAttributeDTO> Attributes { get; set; } = null!;
        public GetCategoryDTO Category { get; set; } = null!;
    }
}
