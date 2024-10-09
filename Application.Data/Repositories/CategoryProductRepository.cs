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
    public class CategoryProductRepository : ICategoryProductRepository
    {
        private readonly GiayDBContext _context;

        public CategoryProductRepository(GiayDBContext context)
        {
            _context = context;
        }
        public async Task Add(CategoryProductDTO categoryProductDto)
        {
            var categoryProduct = new Category_Product
            {
                CategoryProductID = Guid.NewGuid(), // Fake ID
                ProductID = Guid.NewGuid(), // Fake ID
                CategoryID = categoryProductDto.CategoryID
            };

            await _context.Category_Products.AddAsync(categoryProduct);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var categoryProduct = await _context.Category_Products.FindAsync(id);
            if (categoryProduct != null)
            {
                _context.Category_Products.Remove(categoryProduct);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CategoryProductDTO>> GetAll()
        {
            return await _context.Category_Products
            .Select(cp => new CategoryProductDTO
            {
                CategoryProductID = cp.CategoryProductID,
                ProductID = Guid.NewGuid(), // Fake ID
                CategoryID = cp.CategoryID
            }).ToListAsync();
        }

        public async Task<CategoryProductDTO> GetById(Guid id)
        {
            var categoryProduct = await _context.Category_Products.FindAsync(id);
            if (categoryProduct != null)
            {
                return new CategoryProductDTO
                {
                    CategoryProductID = categoryProduct.CategoryProductID,
                    ProductID = Guid.NewGuid(), // Fake ID
                    CategoryID = categoryProduct.CategoryID
                };
            }
            else return default!;
        }

        public async Task Update(CategoryProductDTO categoryProductDto)
        {
            var categoryProduct = await _context.Category_Products.FindAsync(categoryProductDto.CategoryProductID);
            if (categoryProduct == null) return;

            categoryProduct.CategoryID = categoryProductDto.CategoryID;

            _context.Category_Products.Update(categoryProduct);
            await _context.SaveChangesAsync();
        }
    }
}
