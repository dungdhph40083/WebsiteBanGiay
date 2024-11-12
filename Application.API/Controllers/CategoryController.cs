﻿using Application.Data.DTOs;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            return Ok(await _categoryRepository.GetAllCategory());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetById(Guid id)
        {
            var category = await _categoryRepository.GetByIdCategory(id);
            if (category == null) return NotFound();

            return Ok(category);
        }
        [HttpPost("create-category")]
        public async Task<ActionResult> Create(CategoryDTO categoryDto)
        {
            // Gọi phương thức AddCategory và lưu kết quả trả về
            var createdCategory = await _categoryRepository.AddCategory(categoryDto);

            // Sử dụng CreatedAtAction để trả về thông tin đối tượng vừa tạo
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.CategoryID }, createdCategory);
        }

        [HttpPut("update-category/{id}")]
        public async Task<ActionResult> Update(Guid id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryID) return BadRequest();

            await _categoryRepository.UpdateCategory(categoryDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var category = await _categoryRepository.GetByIdCategory(id);
            if (category == null) return NotFound();

            await _categoryRepository.DeleteCategory(id);
            return NoContent();
        }
    }
}
