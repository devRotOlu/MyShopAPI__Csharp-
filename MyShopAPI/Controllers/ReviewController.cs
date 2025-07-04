using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.DTOs.ProductReviewDTOs;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;

namespace MyShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ReviewController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("add-review")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddReview([FromBody] AddReviewDTO reviewDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = _mapper.Map<ProductReview>(reviewDTO);

            await _unitOfWork.ProductReviews.Insert(review);
            await _unitOfWork.Save();

            return Created();
        }

        [HttpPost("edit-review")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditReview([FromBody] AddReviewDTO reviewDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prevReview = await _unitOfWork.ProductReviews.Get(review => review.ProductId == reviewDTO.ProductId && review.ReviewerId == reviewDTO.ReviewerId && review.OrderId == reviewDTO.OrderId);

            if (prevReview == null)
            {
                return NotFound("No previous review.");
            }

            prevReview.Review = reviewDTO.Review;
            prevReview.Rating = reviewDTO.Rating;
            _unitOfWork.ProductReviews.Update(prevReview);

            await _unitOfWork.Save();

            return Ok();
        }

        [HttpGet("list-reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ViewReviews([FromQuery] int productId)
        {
            if (productId <= 0) return BadRequest();

            var result = await _unitOfWork.ProductReviews.GetAll(review => review.ProductId == productId, include: review => review.Include(review => review.Reviewer)).ToListAsync();

            var reviews = _mapper.Map<IEnumerable<ReviewDTO>>(result);

            return Ok(reviews);
        }

        [HttpGet("order-reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrderReviews([FromQuery] int orderId)
        {
            if (orderId <= 0) return BadRequest();

            var orderReviews = await _unitOfWork.ProductReviews.GetAll(review => review.OrderId == orderId)
                .Select(review=> new { review.ProductId,review.Rating,review.Review })
                .ToListAsync();

            return Ok(orderReviews);
        }
    }
}
