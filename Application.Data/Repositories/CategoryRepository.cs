using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GiayDBContext _context;
        private readonly IMapper Mapper;

        public CategoryRepository(GiayDBContext context, IMapper Mapper)
        {
            _context = context;
            this.Mapper = Mapper;
        }
        public async Task<Category> AddCategory(CategoryDTO categoryDto)
        {
            var category = new Category
            {
                CategoryID = Guid.NewGuid(),
                CategoryName = categoryDto.CategoryName,
                Description = categoryDto.Description
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<bool> NameAvailability(string? Name)
        {
            var Searched = await _context.Categories.FirstOrDefaultAsync(Flt => Flt.CategoryName == Name);
            if (Searched != null) return false;
            else return true;
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

        public async Task<List<Category>> GetAllCategory()
        {
            return await _context.Categories.OrderBy(HELP => HELP.CategoryName).ToListAsync();
        }

        public async Task<Category?> GetByIdCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return default;

            return category;
        }

        public async Task<Category?> UpdateCategory(Guid TargetID, CategoryDTO categoryDto)
        {
            var category = await _context.Categories.FindAsync(TargetID);
            if (category != null)
            {
                _context.Categories.Attach(category);
                category = Mapper.Map(categoryDto, category);
                _context.Update(category);
                await _context.SaveChangesAsync();

                return category;
            }
            else return default;
        }
    }
}
