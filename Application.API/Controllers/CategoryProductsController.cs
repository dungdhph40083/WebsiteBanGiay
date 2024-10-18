using Application.Data.DTOs;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryProductsController : ControllerBase
    {
        private readonly ICategoryProductRepository _categoryProductRepository;

        public CategoryProductsController(ICategoryProductRepository categoryProductRepository)
        {
            _categoryProductRepository = categoryProductRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryProductDTO>>> GetAll()
        {
            return Ok(await _categoryProductRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryProductDTO>> GetById(Guid id)
        {
            var categoryProduct = await _categoryProductRepository.GetById(id);
            if (categoryProduct == null) return NotFound();

            return Ok(categoryProduct);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryProductDTO categoryProductDto)
        {
            await _categoryProductRepository.Add(categoryProductDto);
            return CreatedAtAction(nameof(GetById), new { id = categoryProductDto.Category_Products_ID }, categoryProductDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, CategoryProductDTO categoryProductDto)
        {
            if (id != categoryProductDto.Category_Products_ID) return BadRequest();

            await _categoryProductRepository.Update(categoryProductDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _categoryProductRepository.Delete(id);
            return NoContent();
        }
    }
}
