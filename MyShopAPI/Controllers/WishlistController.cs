using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.DTOs.WishlistDTOs;
using MyShopAPI.Core.EntityDTO.WishlistDTOs;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;
using MyShopAPI.Helpers;

namespace MyShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public WishlistController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("add_item")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToWishlist([FromBody] AddWishlistDTO item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _unitOfWork.Wishlists.Get(listItem => listItem.ProductId == item.ProductId && listItem.CustomerId == item.CustomerId);

            if (result == null)
            {
                var wishlisttem = _mapper.Map<Wishlist>(item);

                await _unitOfWork.Wishlists.Insert(wishlisttem);
            }
            else
            {
                result.isDeleted = false;

                result.AddedAt = DateTime.UtcNow;

                _unitOfWork.Wishlists.Update(result);
            }

            await _unitOfWork.Save();

            return Created();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetWishListItems([FromQuery] string email)
        {
            var customer = await _unitOfWork.Customers.Get(customer => customer.Email == email);

            if (customer == null)
            {
                return BadRequest();
            }

            var results = await _unitOfWork.Wishlists.GetAll(item => item.CustomerId == customer.Id && !item.isDeleted, include: item => item.Include(item => item.Product)
                                                    .ThenInclude(product => product.Images)
                                                .Include(item => item.Product)
                                                    .ThenInclude(product => product.Reviews))
                                                .ToListAsync();

            var cartItems = _mapper.Map<IEnumerable<GetWishlistDTO>>(results);

            return Ok(cartItems);
        }

        [HttpDelete("delete_item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteWishListItem([FromQuery] string customerId, [FromQuery] int productId)
        {
            if (String.IsNullOrEmpty(customerId) || productId <= 0) return BadRequest();

            var item = await _unitOfWork.Wishlists.Get(item => item.CustomerId == customerId && item.ProductId == productId);

            if (item == null) return BadRequest();

            item.isDeleted = true;
            item.DeletedAt = DateTime.UtcNow;

            _unitOfWork.Wishlists.Update(item);

            await _unitOfWork.Save();

            return Ok();
        }

        [HttpGet("isWishlist_item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ContainsProduct([FromQuery] int productId, [FromQuery] string customerId)
        {
            if (productId <= 0)
                return BadRequest();

            var item = await _unitOfWork.Wishlists.Get(item => item.Product.Id == productId && item.CustomerId == customerId && !item.isDeleted);

            var isWishlistItem = item == null ? false : true;

            return Ok(new { isWishlistItem });
        }

    }
}
