using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.EntityDTO.WishlistDTO;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;

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

            var cartItem = _mapper.Map<Wishlist>(item);

            await _unitOfWork.Wishlists.Insert(cartItem);
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

            var results = await _unitOfWork.Wishlists.GetAll(item => item.CustomerId == customer.Id, include: item => item.Include(item => item.Product)
                                            .ThenInclude(product => product.Images));

            var cartItems = _mapper.Map<IEnumerable<GetWishlistDTO>>(results);

            return Ok(cartItems);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteWishListItem(AddWishlistDTO wishlistDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var item = await _unitOfWork.Wishlists.Get(item=>item.CustomerId == wishlistDTO.CustomerId && item.ProductId == wishlistDTO.ProductId);

            if (item == null) return BadRequest();

            _unitOfWork.Wishlists.Delete(item);

            await _unitOfWork.Save();

            return Ok();
        }
    }
}
