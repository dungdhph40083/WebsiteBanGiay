using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategory();
        Task<Category?> GetByIdCategory(Guid id);
        Task<Category> AddCategory(CategoryDTO categoryDto);
        Task<Category?> UpdateCategory(Guid TargetID, CategoryDTO categoryDto);
        Task DeleteCategory(Guid id);
    }
}
