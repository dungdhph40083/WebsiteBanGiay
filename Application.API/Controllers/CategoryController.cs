using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            return Ok(await _categoryRepository.GetAllCategory());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<Category>> GetById(Guid id)
        {
            var category = await _categoryRepository.GetByIdCategory(id);
            if (category == null) return NotFound();

            return Ok(category);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(CategoryDTO categoryDto)
        {
            // Gọi phương thức AddCategory và lưu kết quả trả về
            var createdCategory = await _categoryRepository.AddCategory(categoryDto);

            // Sử dụng CreatedAtAction để trả về thông tin đối tượng vừa tạo
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.CategoryID }, createdCategory);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(Guid id, CategoryDTO categoryDto)
        {
            // if (GetById(id) == null) return BadRequest();
            return Ok(await _categoryRepository.UpdateCategory(id, categoryDto));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var category = await _categoryRepository.GetByIdCategory(id);
            if (category == null) return NotFound();

            await _categoryRepository.DeleteCategory(id);
            return NoContent();
        }
    }
}
