using MyShopAPI.Core.DTOs.UserDTOs;

namespace MyShopAPI.Core.DTOs.ProductReviewDTOs
{
    public class ReviewDTO : ProductReviewDTO
    {
        public ReviewerDTO Reviewer { get; set; } = null!;
        public string ReviewDate { get; set; } = null!;
    }
}
