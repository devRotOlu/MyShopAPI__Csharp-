using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.DTOs.CategoryDTOs;
using MyShopAPI.Core.IRepository;

namespace MyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork,IMapper mapper)
        {
          _unitOfWork = unitOfWork;
          _mapper = mapper;
        }

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _unitOfWork.Categories.GetAll().ToListAsync();

            var categoriesDTO = _mapper.Map<IEnumerable<GetCategoryDTO>>(categories);

            return Ok(categoriesDTO);
        }
    }
}
