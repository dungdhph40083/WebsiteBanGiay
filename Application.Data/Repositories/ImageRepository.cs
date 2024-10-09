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
                CreatedDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return new ImageDTO
            {
                ImageID = image.ImageID,
                ImageName = image.ImageName,
                Status = image.Status,
                CreatedDate = image.CreatedDate,
                UpdateDate = image.UpdateDate
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
                CreatedDate = i.CreatedDate,
                UpdateDate = i.UpdateDate
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
                CreatedDate = image.CreatedDate,
                UpdateDate = image.UpdateDate
            };
        }

        public  async Task<bool> UpdateImageAsync(Guid imageId, ImageDTO imageDto)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null) return false;

            image.ImageName = imageDto.ImageName;
            image.Status = imageDto.Status;
            image.UpdateDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
