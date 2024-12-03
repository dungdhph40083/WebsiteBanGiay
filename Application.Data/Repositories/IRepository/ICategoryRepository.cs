using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategory();
        Task<CategoryDTO> GetByIdCategory(Guid id);
        Task<CategoryDTO> AddCategory(CategoryDTO categoryDto);
        Task UpdateCategory(CategoryDTO categoryDto);
        Task DeleteCategory(Guid id);
    }
}
