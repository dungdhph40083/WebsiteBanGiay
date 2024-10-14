using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GiayDBContext _context;

        public CategoryRepository(GiayDBContext context)
        {
            _context = context;
        }
        public async Task AddCategory(CategoryDTO categoryDto)
        {
            var category = new Category
            {
                CategoryID = Guid.NewGuid(), // Fake ID
                CategoryName = categoryDto.CategoryName,
                Description = categoryDto.Description
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
        {
            return await _context.Categories
             .Select(c => new CategoryDTO
             {
                 CategoryID = c.CategoryID,
                 CategoryName = c.CategoryName,
                 Description = c.Description
             }).ToListAsync();
        }

        public async Task<CategoryDTO> GetByIdCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return default!;

            return new CategoryDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
        }

        public async Task UpdateCategory(CategoryDTO categoryDto)
        {
            var category = await _context.Categories.FindAsync(categoryDto.CategoryID);
            if (category == null) return;

            category.CategoryName = categoryDto.CategoryName;
            category.Description = categoryDto.Description;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
