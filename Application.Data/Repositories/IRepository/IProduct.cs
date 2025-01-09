using Application.Data.DTOs;
using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IProduct
    {
        Task<List<Product>> GetAll();
        Task<Product?> GetById(Guid id);
        Task<Product> Add(ProductDTO ProductDTO);
        Task<Product?> Update(Guid id,ProductDTO ProductDTO);
        Task Delete(Guid id);
        // Thêm phương thức kiểm tra tên sản phẩm
        Task<Product?> CheckProductNameAsync(string name);
    }
}
