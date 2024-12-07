using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.EntityDTO.ProductReviewDTO;
using MyShopAPI.Core.EntityDTO.ProoductDTO;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;
using MyShopAPI.Services.Image;

namespace MyShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IMapper mapper, IPhotoService photoService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _photoService = photoService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("add-product")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productDTO);

            product.Images = new List<ProductImage>();

            foreach (var file in productDTO.Photos)
            {
                var photo = await _photoService.AddPhotoAsync(file);

                if (photo.Error != null) return BadRequest(photo.Error.Message);

                product.Images.Add(new ProductImage
                {
                    Url = photo.SecureUrl.AbsoluteUri,
                    PublicId = photo.PublicId,
                });
            }

            await _unitOfWork.Products.Insert(product);

            await _unitOfWork.Save();

            return Created();
        }

        [HttpGet("list-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts()
        {
            var results = await _unitOfWork.Products.GetAll(product => product.Quantity != 0, include: product => product.Include(product => product.Images));

            if (results == null)
            {
                return BadRequest();
            }

            var products = _mapper.Map<IEnumerable<GetProductDTO>>(results);
            return Ok(products);
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

        [HttpGet("list-reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ViewReviews([FromQuery] int productId)
        {
            if (productId <= 0) return BadRequest();

            var result = await _unitOfWork.ProductReviews.GetAll(review => review.ProductId == productId,include:review=>review.Include(review=>review.Reviewer));

            var reviews = _mapper.Map<IEnumerable<ReviewDTO>>(result);

            return Ok(reviews);
        }
    }
}
