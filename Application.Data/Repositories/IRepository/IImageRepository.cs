using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Data.Repositories.IRepository
{
    public interface IImageRepository
    {
        Task<List<Image>> GetAllImagesAsync();
        Task<List<Image>> GetImagesByProductID(Guid ProductID);
        Task<Image?> GetImageByIdAsync(Guid imageId);
        Task<Image> CreateImageAsync(IFormFile File);
        Task<List<Image>> CreateShittonOfImagesWithProductIDAsync(Guid? ProductID, List<IFormFile> Files);
        Task<bool> DeleteImageAsync(Guid imageId);
    }
}
