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
    public class ProductRepository : IProduct
    {
        private readonly GiayDBContext _context;

        public ProductRepository(GiayDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(Guid id)
        {
            return _context.Products.Include(p => p.Image).FirstOrDefault(p => p.ProductID == id);
        }

        public void Add(Product product)
        {
            if (product.ImageID.HasValue)
            {
                product.Image = _context.Images.Find(product.ImageID);
            }
            _context.Products.Add(product);
        }

        public void Update(Guid ID, Product product)
        {
            var Target = _context.Products.Find(ID);

            if (Target == null)
            {
                throw new Exception("Product not found."); 
            }

            _context.Entry(Target).State = EntityState.Modified;

            // Cập nhật các trường thông tin của sản phẩm
            Target.Name = product.Name;
            Target.Description = product.Description;
            Target.Price = product.Price;
            Target.ImageID = product.ImageID;
            Target.UpdatedAt = DateTime.UtcNow;
            _context.Update(Target);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
