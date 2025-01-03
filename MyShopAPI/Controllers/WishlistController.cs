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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToWishlist([FromBody] AddWishlistDTO item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _unitOfWork.Products.Get(product => product.Id == item.ProductId && product.Quantity > 0);

            if (result == null) return BadRequest();

            var cartItem = _mapper.Map<CartAndWishlist>(item);

            await _unitOfWork.CartsAndWishlists.Insert(cartItem);
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

            var results = await _unitOfWork.CartsAndWishlists.GetAll(item => item.CustomerId == customer.Id && item.Quantity == 0, include: item => item.Include(item => item.Product)
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

            var item = await _unitOfWork.CartsAndWishlists.Get(item=>item.CustomerId == wishlistDTO.CustomerId && item.ProductId == wishlistDTO.ProductId && item.Quantity == 0);

            if (item == null) return BadRequest();

            _unitOfWork.CartsAndWishlists.Delete(item);

            await _unitOfWork.Save();

            return Ok();
        }
    }
}
