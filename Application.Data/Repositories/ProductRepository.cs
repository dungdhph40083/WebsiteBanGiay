using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly GiayDBContext _context;
        private readonly IMapper Mapper;

        public ProductRepository(GiayDBContext context, IMapper Mapper)
        {
            _context = context;
            this.Mapper = Mapper;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(Guid id)
        {
            return _context.Products.Include(p => p.Image).FirstOrDefault(p => p.ProductID == id);
        }

        public Product Add(ProductDTO ProductDTO)
        {
            var DateTimeUtcNow = DateTime.UtcNow;

            Product Product = new()
            {
                ProductID = Guid.NewGuid(),
                CreatedAt = DateTimeUtcNow,
                UpdatedAt = DateTimeUtcNow
            };

            Product = Mapper.Map(ProductDTO, Product);

            _context.Products.Add(Product);
            _context.SaveChanges();

            return Product;
        }

        public Product Update(Guid ID, ProductDTO ProductDTO)
        {
            var Target = _context.Products.Find(ID);

            if (Target == null)
            {
                throw new Exception("Product not found."); 
            }

            _context.Entry(Target).State = EntityState.Modified;

            // Cập nhật các trường thông tin của sản phẩm

            Target = Mapper.Map(ProductDTO, Target);

            _context.Update(Target);
            _context.SaveChanges();

            return Target;
        }

        public void Delete(Guid id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
