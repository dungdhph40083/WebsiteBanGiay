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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            return Ok(await _categoryRepository.GetAllCategory());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> GetById(Guid id)
        {
            var category = await _categoryRepository.GetByIdCategory(id);
            if (category == null) return NotFound();

            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult> Create(CategoryDTO categoryDto)
        {
            var Check = await _categoryRepository.NameAvailability(categoryDto.CategoryName);
            if (!Check) return Conflict();

            // Gọi phương thức AddCategory và lưu kết quả trả về
            var createdCategory = await _categoryRepository.AddCategory(categoryDto);

            // Sử dụng CreatedAtAction để trả về thông tin đối tượng vừa tạo
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.CategoryID }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, CategoryDTO categoryDto)
        {
            var OldCategory = await _categoryRepository.GetByIdCategory(id);
            if (!string.Equals(categoryDto.CategoryName, OldCategory?.CategoryName, StringComparison.OrdinalIgnoreCase))
            {
                var Check = await _categoryRepository.NameAvailability(categoryDto.CategoryName);
                if (!Check) return Conflict();
            }

            return Ok(await _categoryRepository.UpdateCategory(id, categoryDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _categoryRepository.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception)
            {
                var category = await _categoryRepository.GetByIdCategory(id);
                if (category == null) return NoContent();
                else return Conflict();
            }
        }
    }
}
