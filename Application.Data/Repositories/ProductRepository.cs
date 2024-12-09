using Application.Data.DTOs;
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
                Context.Entry(Target).State = EntityState.Modified;
                Target.UpdatedAt = DateTime.UtcNow;

                // nếu ko có ảnh trong form thì nó sẽ ko xóa ảnh
                // http patch cũng ko được đâu
                // god dam mit bro....
                if (ProductDTO.ImageID == null) ProductDTO.ImageID = Target.ImageID;

                Target = Mapper.Map(ProductDTO, Target);
                Context.Update(Target);
                await Context.SaveChangesAsync();
                return Target;
            }
            else return default;
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
    }
}
