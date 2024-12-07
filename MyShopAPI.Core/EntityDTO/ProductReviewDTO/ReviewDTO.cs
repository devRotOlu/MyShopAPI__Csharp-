using MyShopAPI.Core.EntityDTO.UserDTO;

namespace MyShopAPI.Core.EntityDTO.ProductReviewDTO
{
    public class ReviewDTO : ProductReviewDTO
    {
        public ReviewerDTO Reviewer { get; set; } = null!;
    }
}
