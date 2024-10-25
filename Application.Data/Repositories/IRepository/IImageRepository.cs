using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Data.Repositories.IRepository
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllImagesAsync();
        Task<Image?> GetImageByIdAsync(Guid imageId);
        Task<Image> CreateImageAsync(ImageDTO imageDto, IFormFile File);
        Task<bool> UpdateImageAsync(Guid imageId, ImageDTO imageDto);
        Task<bool> DeleteImageAsync(Guid imageId);
    }
}
