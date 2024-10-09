using Application.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IImageRepository
    {
        Task<IEnumerable<ImageDTO>> GetAllImagesAsync();
        Task<ImageDTO?> GetImageByIdAsync(Guid imageId);
        Task<ImageDTO> CreateImageAsync(ImageDTO imageDto);
        Task<bool> UpdateImageAsync(Guid imageId, ImageDTO imageDto);
        Task<bool> DeleteImageAsync(Guid imageId);
    }
}
