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
        IEnumerable<Product> GetAll();
        Product GetById(Guid id);
        Product Add(ProductDTO ProductDTO);
        Product Update(Guid id,ProductDTO ProductDTO);
        void Delete(Guid id);
    }
}
