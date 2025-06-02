using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.DTOs.OrderDTOs;
using MyShopAPI.Core.IRepository;

namespace MyShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrders([FromQuery] string customerId)
        {
            if (string.IsNullOrEmpty(customerId)) return BadRequest();

            var orders = await _unitOfWork.Orders.GetAll(order => order.CustomerId == customerId,
                include: order => order.Include(order => order.DeliveryProfile)
                .Include(order => order.CartItems)
                .ThenInclude(item => item.CartItem)
                .ThenInclude(cart => cart.Product)
                .ThenInclude(product => product.Images)).ToListAsync();

            var ordersDTO = _mapper.Map<IEnumerable<GetOrderDTO>>(orders);

            return Ok(ordersDTO);
        }
    }
}
