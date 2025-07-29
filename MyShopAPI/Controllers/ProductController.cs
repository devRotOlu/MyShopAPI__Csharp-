using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.DTOs.ProductAttributeDTOs;
using MyShopAPI.Core.DTOs.ProductDTOs;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;
using MyShopAPI.Helpers;
using MyShopAPI.Services.Image;
using Newtonsoft.Json;

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

        [AllowAnonymous]
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

            if (productDTO.AttributesJson != null)
            {
                var attributes = JsonConvert.DeserializeObject<IEnumerable<ProductAttributeDTO>>(productDTO.AttributesJson);

                var _product = _mapper.Map<Product>(attributes);

                product.Attributes = _product.Attributes;
            }

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

        [AllowAnonymous]
        [HttpGet("list-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProducts()
        {
            var results = await _unitOfWork.Products.GetAll(product => product.Quantity != 0, include: product => product.Include(product => product.Images)
             .Include(product => product.Reviews)).ToListAsync();

            var products = _mapper.Map<IEnumerable<GetProductDTO>>(results);

            return Ok(products);
        }


        [AllowAnonymous]
        [HttpGet("get-product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProduct([FromQuery] int productId)
        {
            var result = await _unitOfWork.Products.Get(product => product.Quantity != 0 && product.Id == productId, include: product => product.Include(product => product.Images)
                 .Include(product => product.Category)
                 .Include(product => product.Reviews)
                    .ThenInclude(review => review.Reviewer)
                    .ThenInclude(reviewer => reviewer.Details)
                 .Include(product => product.Attributes)
                    .ThenInclude(attribute => attribute.Attribute));

            var product = _mapper.Map<GetProductDTO>(result);

            return Ok(product);
        }

        [HttpPatch("update-product")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct(GetProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productDTO);

            _unitOfWork.Products.Update(product);

            await _unitOfWork.Save();

            return NoContent();
        }

        [AllowAnonymous]
        [HttpGet("brand_products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBrandProducts([FromQuery] string brand, [FromQuery] int? min = 0, [FromQuery] int? max = int.MaxValue, [FromQuery] int? rating = 0, [FromQuery] string? sortBy = null, [FromQuery] string? sortOrder = null)
        {
            if (String.IsNullOrEmpty(brand))
            {
                return BadRequest();
            }

            var allowedFields = new[] { "Name", "UnitPrice" };

            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null;

            if (!string.IsNullOrWhiteSpace(sortBy) &&
        allowedFields.Contains(sortBy, StringComparer.OrdinalIgnoreCase))
            {
                bool descending = sortOrder?.ToLower() == "desc";
                orderBy = q => q.OrderByProperty(sortBy, descending);
            }

            var results = await _unitOfWork.Products.GetAll(product => product.Attributes != null && product.Attributes.Any(attribute => attribute.Value == brand) && min <= product.UnitPrice
              && product.UnitPrice <= max && product.AverageRating >= rating, orderBy: orderBy, include: product => product.Include(product => product.Images)
                 .Include(product => product.Reviews)
                    .ThenInclude(review => review.Reviewer)
                 .Include(product => product.Category)).ToListAsync();


            var products = _mapper.Map<IEnumerable<GetProductDTO>>(results);

            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("category_products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoryProducts([FromQuery] int categoryId, [FromQuery] int? min = 0, [FromQuery] int? max = int.MaxValue, [FromQuery] int? rating = 0, [FromQuery] string? sortBy = null, [FromQuery] string? sortOrder = null)
        {
            if (categoryId <= 0)
            {
                return BadRequest();
            }

            var allowedFields = new[] { "Name", "UnitPrice" };

            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null;

            if (!string.IsNullOrWhiteSpace(sortBy) &&
        allowedFields.Contains(sortBy, StringComparer.OrdinalIgnoreCase))
            {
                bool descending = sortOrder?.ToLower() == "desc";
                orderBy = q => q.OrderByProperty(sortBy, descending);
            }

            var results = await _unitOfWork.Products.GetAll(product => product.CategoryId == categoryId && min <= product.UnitPrice && product.UnitPrice <= max && product.AverageRating >= rating, orderBy: orderBy, include: product => product.Include(product => product.Images)
                 .Include(product => product.Reviews)
                 .Include(product => product.Category)).ToListAsync();

            var result = await _unitOfWork.Categories.Get(category => category.Id == categoryId);

            var products = _mapper.Map<IEnumerable<GetProductDTO>>(results);

            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("product_search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchProductByNameCategoryorBrand([FromQuery] string searchTerm)
        {
            if (String.IsNullOrEmpty(searchTerm))
            {
                return BadRequest();
            }

            var products = await _unitOfWork.Products.GetAll(product => EF.Functions.ILike(product.Name, $"%{searchTerm}%") || EF.Functions.ILike(product.Category.Name, $"%{searchTerm}%"), include: product => product.Include(product => product.Images)).ToListAsync();

            var brands = await _unitOfWork.ProductAttributes.GetAll(attribute => attribute.Attribute.Name == "brand" &&
                EF.Functions.ILike(attribute.Value, $"%{searchTerm}%"), include: attribute => attribute.Include(attribute => attribute.Attribute))
                    .Select(attribute => attribute.Value)
                    .Distinct()
                    .ToListAsync();

            var _products = _mapper.Map<IEnumerable<GetProductDTO>>(products);

            return Ok(new { products = _products, brands });
        }
    }
}
