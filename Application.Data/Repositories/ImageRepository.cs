using Application.Data.DTOs;
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
    public class ImageRepository : IImageRepository
    {
        private readonly GiayDBContext _context;

        public ImageRepository(GiayDBContext context)
        {
            _context = context;
        }
        public async Task<ImageDTO> CreateImageAsync(ImageDTO imageDto)
        {
            var image = new Image
            {
                ImageID = Guid.NewGuid(), // Tạo ID mới
                ImageName = imageDto.ImageName,
                Status = imageDto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return new ImageDTO
            {
                ImageID = image.ImageID,
                ImageName = image.ImageName,
                Status = image.Status,
                CreatedAt = image.CreatedAt,
                UpdatedAt = image.UpdatedAt
            };
        }

        public async Task<bool> DeleteImageAsync(Guid imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null) return false;

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ImageDTO>> GetAllImagesAsync()
        {
            return await _context.Images
            .Select(i => new ImageDTO
            {
                ImageID = i.ImageID,
                ImageName = i.ImageName,
                Status = i.Status,
                CreatedAt = i.CreatedAt,
                UpdatedAt = i.UpdatedAt
            }).ToListAsync();
        }

        public async Task<ImageDTO?> GetImageByIdAsync(Guid imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null) return null;

            return new ImageDTO
            {
                ImageID = image.ImageID,
                ImageName = image.ImageName,
                Status = image.Status,
                CreatedAt = image.CreatedAt,
                UpdatedAt = image.UpdatedAt
            };
        }

        public  async Task<bool> UpdateImageAsync(Guid imageId, ImageDTO imageDto)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null) return false;

            image.ImageName = imageDto.ImageName;
            image.Status = imageDto.Status;
            image.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
