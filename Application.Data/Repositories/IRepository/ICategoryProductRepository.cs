using Application.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface ICategoryProductRepository
    {
        Task<IEnumerable<CategoryProductDTO>> GetAll();
        Task<CategoryProductDTO> GetById(Guid id);
        Task Add(CategoryProductDTO categoryProductDto);
        Task Update(CategoryProductDTO categoryProductDto);
        Task Delete(Guid id);
    }
}
