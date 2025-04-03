using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.EntityDTO.OrderDTO;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Data.Entities;

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

        [HttpPost("add_order")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderDTO orderDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cartItems = await _unitOfWork.Carts.GetAll(item => item.CustomerId == orderDTO.CustomerId && item.Quantity != 0);

            var mappedOrder = _mapper.Map<CustomerOrder>(orderDTO);

            mappedOrder.ItemsOrdered = cartItems;

            await _unitOfWork.Orders.Insert(mappedOrder);

            await _unitOfWork.Save();

            return Created();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrders([FromQuery] string customerId)
        {
            if (string.IsNullOrEmpty(customerId)) return BadRequest();

            var orders = await _unitOfWork.Orders.GetAll(order => order.CustomerId == customerId,
                include:order=>order.Include(order=>order.DeliveryProfile)
                .Include(order=>order.ItemsOrdered)
                .ThenInclude(item=>item.Product)
                .ThenInclude(product=>product.Images));

            var ordersDTO = _mapper.Map<IEnumerable<GetOrderDTO>>(orders);

            return Ok(ordersDTO);
        }
    }
}
