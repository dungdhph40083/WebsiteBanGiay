﻿using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;

        public ProductRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        public async Task<List<Product>> GetAll()
        {
            return await Context.Products
                .Include(UU => UU.Image).ToListAsync();
        }

        public async Task<Product?> GetById(Guid id)
        {
            return await Context.Products
                .Include(UU => UU.Image).SingleOrDefaultAsync
                (x => x.ProductID == id);
        }

        public async Task<Product> Add(ProductDTO ProductDTO)
        {
            var DateTimeUtcNow = DateTime.UtcNow;

            Product Product = new()
            {
                ProductID = Guid.NewGuid(),
                CreatedAt = DateTimeUtcNow,
                UpdatedAt = DateTimeUtcNow
            };

            Product = Mapper.Map(ProductDTO, Product);

            await Context.Products.AddAsync(Product);
            await Context.SaveChangesAsync();

            return Product;
        }

        public async Task<Product?> Update(Guid ID, ProductDTO ProductDTO)
        {
            var Target = await GetById(ID);

            if (Target != null)
            {
                // Kiểm tra nếu tên sản phẩm thay đổi
                if (!string.Equals(Target.Name, ProductDTO.Name, StringComparison.OrdinalIgnoreCase))
                {
                    // Kiểm tra tên mới có bị trùng với sản phẩm khác không
                    var existingProduct = await CheckProductNameAsync(ProductDTO.Name);

                    if (existingProduct != null)
                    {
                        // Nếu có sản phẩm trùng tên, trả về null hoặc ném lỗi
                        return null; // Hoặc throw new InvalidOperationException("Tên sản phẩm đã tồn tại.");
                    }
                }

                // Nếu tên không thay đổi hoặc không trùng, tiếp tục cập nhật các thuộc tính khác
                Target.Name = ProductDTO.Name;
                Target.Description = ProductDTO.Description;
                Target.Price = ProductDTO.Price;
                Target.UpdatedAt = DateTime.UtcNow;

                // Cập nhật dữ liệu bằng AutoMapper
                Target = Mapper.Map(ProductDTO, Target);

                // Lưu thay đổi vào cơ sở dữ liệu
                Context.Products.Update(Target);
                await Context.SaveChangesAsync();
                return Target;
            }

            return null; // Nếu không tìm thấy sản phẩm
        }


        public async Task Delete(Guid id)
        {
            var Product = await GetById(id);
            if (Product != null)
            {
                Context.Products.Remove(Product);
                await Context.SaveChangesAsync();
            }
        }
        public async Task<Product?> CheckProductNameAsync(string name)
        {
            return await Context.Products
                .FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
        }


    }
}
