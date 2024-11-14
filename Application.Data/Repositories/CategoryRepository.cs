﻿using Application.Data.DTOs;
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
            Category Category = new(){ CategoryID = Guid.NewGuid() };
            Category = Mapper.Map(categoryDto, Category);
            await _context.Categories.AddAsync(Category);
            await _context.SaveChangesAsync();
            return Category;
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
            return await _context.Categories.ToListAsync();
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
            if (category == null) return default;
            _context.Entry(category).State = EntityState.Modified;
            var UpdatedTarget = Mapper.Map(categoryDto, category);
            _context.Update(UpdatedTarget);
            await _context.SaveChangesAsync();

            return UpdatedTarget;
        }
    }
}
