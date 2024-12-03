using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.EntityDTO.CartDTO;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;

namespace MyShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToCart([FromBody] AddCartDTO item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _unitOfWork.Products.Get(product => product.Id == item.ProductId && product.Quantity > 0);

            if (result == null) return BadRequest();

            var cartItem = _mapper.Map<Cart>(item);

            await _unitOfWork.Carts.Insert(cartItem);

            await _unitOfWork.Save();

            return Created();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCartItems([FromQuery] string email)
        {
            var customer = await _unitOfWork.Customers.Get(customer => customer.Email == email);

            if (customer == null)
            {
                return BadRequest();
            }

            var results = await _unitOfWork.Carts.GetAll(item => item.CustomerId == customer.Id, include: item => item.Include(item => item.Product)
                                            .ThenInclude(product => product.Images));

            var cartItems = _mapper.Map<IEnumerable<GetCartDTO>>(results);

            return Ok(cartItems);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCartItem([FromBody] AddCartDTO cartDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cartItem = _mapper.Map<Cart>(cartDTO);

            _unitOfWork.Carts.Update(cartItem);

            await _unitOfWork.Save();

            return Ok(cartItem);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCartItem(AddCartDTO cartDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cartItem = _mapper.Map<Cart>(cartDTO);

            _unitOfWork.Carts.Delete(cartItem);

            await _unitOfWork.Save();

            return Ok();
        }
    }
}
