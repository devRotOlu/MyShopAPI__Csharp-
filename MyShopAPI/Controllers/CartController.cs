using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.DTOs.CartDTOs;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;
using MyShopAPI.Helpers;

namespace MyShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CartController> _logger;

        public CartController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CartController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("add_item")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddToCart([FromBody] AddCartDTO item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _unitOfWork.Carts.Get(
                    cart => cart.ProductId == item.ProductId && cart.CustomerId == item.CustomerId);

                if (result == null)
                {
                    var cartItem = _mapper.Map<Cart>(item);

                    await _unitOfWork.Carts.Insert(cartItem);
                }
                else
                {
                    result.Quantity = item.Quantity;
                    result.DeletedAt = null;
                    result.AddedAt = DateTimeManager.GetNativeDateTime();

                    _unitOfWork.Carts.Update(result);
                }

                await _unitOfWork.Save();

                return Created(string.Empty, null); // Created() overload requires URI or object
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to add an item to cart. ProductId: {ProductId}, CustomerId: {CustomerId}",
                    item.ProductId, item.CustomerId);

                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        //[HttpPost("add_item")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> AddToCart([FromBody] AddCartDTO item)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    var result = await _unitOfWork.Carts.Get(cart => cart.ProductId == item.ProductId && cart.CustomerId == item.CustomerId);

        //    if (result == null)
        //    {
        //        var cartItem = _mapper.Map<Cart>(item);

        //        await _unitOfWork.Carts.Insert(cartItem);
        //    }
        //    else
        //    {
        //        result.Quantity = item.Quantity;
        //        result.DeletedAt = null;
        //        result.AddedAt = DateTimeManager.GetNativeDateTime();

        //        _unitOfWork.Carts.Update(result);
        //    }

        //    await _unitOfWork.Save();

        //    return Created();
        //}

        [HttpPost("add_items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddToCart([FromBody] IEnumerable<AddCartDTO> items)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var productIds = items.Select(cart => cart.ProductId).ToList();

            var customerId = items.ToList()[0].CustomerId;

            // get all soft-deleted items 
            var existingCartItems = await _unitOfWork.Carts.GetAll()
                .Where(cart => cart.CustomerId == customerId && productIds.Contains(cart.ProductId))
                .ToListAsync();

            foreach (var item in items)
            {
                var existingItem = existingCartItems
                    .FirstOrDefault(cart => cart.ProductId == item.ProductId);

                if (existingItem == null)
                {
                    var cartItem = _mapper.Map<Cart>(item);
                    await _unitOfWork.Carts.Insert(cartItem);
                }
                else
                {
                    existingItem.Quantity = item.Quantity;
                    existingItem.DeletedAt = null;
                    existingItem.AddedAt = DateTimeManager.GetNativeDateTime();
                    _unitOfWork.Carts.Update(existingItem);
                }
            }

            await _unitOfWork.Save();

            return Created();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCartItems([FromQuery] string email)
        {
            var customer = await _unitOfWork.Customers.Get(customer => customer.Email == email);

            if (customer == null)
            {
                return BadRequest();
            }

            var results = await _unitOfWork.Carts.GetAll(item => item.CustomerId == customer.Id && item.Quantity != 0, include: item => item.Include(item => item.Product)
                    .ThenInclude(product => product.Images)).ToListAsync();

            var cartItems = _mapper.Map<IEnumerable<GetCartDTO>>(results);

            return Ok(cartItems);
        }

        [HttpPut("update_item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCartItem([FromQuery] int id)
        {
            if (id <= 0) return BadRequest();

            var cartItem = await _unitOfWork.Carts.Get(item => item.Id == id);

            if (cartItem == null)
            {
                return BadRequest();
            }

            // soft delete
            cartItem.Quantity = 0;
            cartItem.DeletedAt = DateTimeManager.GetNativeDateTime();
            _unitOfWork.Carts.Update(cartItem);
            await _unitOfWork.Save();
            return Ok();
        }

        [HttpPost("move_to_wishlist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MoveToWishlist([FromQuery] int cartId)
        {
            if (cartId <= 0) return BadRequest();

            var cartItem = await _unitOfWork.Carts.Get(item => item.Id == cartId);

            if (cartItem == null) return NotFound();

            var item = await _unitOfWork.Wishlists
                .Get(item => item.CustomerId == cartItem.CustomerId && item.ProductId == cartItem.ProductId);

            var _dateTime = DateTimeManager.GetNativeDateTime();

            if (item == null)
            {
                var wishlitItem = new Wishlist
                {
                    CustomerId = cartItem.CustomerId,
                    ProductId = cartItem.ProductId,
                    AddedAt = _dateTime,
                };
                await _unitOfWork.Wishlists.Insert(wishlitItem);
            }
            else if (item.isDeleted)
            {
                item.isDeleted = false;
                item.DeletedAt = null;
                item.AddedAt = _dateTime;
                _unitOfWork.Wishlists.Update(item);
            }
            else
            {
                return Conflict("Item already exits");
            }

            cartItem.Quantity = 0;
            cartItem.DeletedAt = _dateTime;
            _unitOfWork.Carts.Update(cartItem);
            await _unitOfWork.Save();
            return Ok("Moved to wishlist");
        }
    }
}
