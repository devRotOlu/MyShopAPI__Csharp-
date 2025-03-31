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

        [HttpPost("add_item")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToCart([FromBody] AddCartDTO item)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cartItem = _mapper.Map<Cart>(item);

            await _unitOfWork.Carts.Insert(cartItem);

            await _unitOfWork.Save();

            return Created();
        }

        [HttpPost("add_items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddToCart([FromBody] IEnumerable<AddCartDTO> items)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cartItems = _mapper.Map<IEnumerable<Cart>>(items);

            await _unitOfWork.Carts.InsertRange(cartItems);

            await _unitOfWork.Save();

            return Created();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCartItems([FromQuery] string email)
        {
            var customer = await _unitOfWork.Customers.Get(customer => customer.Email == email);

            if (customer == null)
            {
                return BadRequest();
            }

            var results = await _unitOfWork.Carts.GetAll(item => item.CustomerId == customer.Id && item.Quantity != 0, include: item => item.Include(item => item.Product)
                    .ThenInclude(product => product.Images));

            var cartItems = _mapper.Map<IEnumerable<GetCartDTO>>(results);

            return Ok(cartItems);
        }

        [HttpPut("update_item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartDTO cartDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cartItem = _mapper.Map<Cart>(cartDTO);

            _unitOfWork.Carts.Update(cartItem);

            await _unitOfWork.Save();

            return Ok(cartItem);
        }

        [HttpPut("update_items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCartItems([FromBody] IEnumerable<UpdateCartDTO> cartDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cartItems = _mapper.Map<IEnumerable<Cart>>(cartDTO);

            _unitOfWork.Carts.UpdateRange(cartItems);

            await _unitOfWork.Save();

            return Ok(cartItems);

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCartItem([FromQuery] int id)
        {
            if (id <= 0) return BadRequest();

            var cartItem = await _unitOfWork.Carts.Get(item=> item.Id == id);

            if (cartItem == null)
            {
                return BadRequest();
            }

            _unitOfWork.Carts.Delete(cartItem);

            await _unitOfWork.Save();

            return Ok();
        }
    }
}
