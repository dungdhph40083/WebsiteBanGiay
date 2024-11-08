﻿using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly GiayDBContext _context;
        private readonly IMapper Mapper;
        private static Random RNG = new Random();
        const string RandomizerChars = "0123456789abcdef";

        public ImageRepository(GiayDBContext context, IMapper Mapper)
        {
            _context = context;
            this.Mapper = Mapper;
        }
        public async Task<Image> CreateImageAsync(ImageDTO imageDto, IFormFile ImageFile)
        {
            DateTime TimeSync = DateTime.UtcNow;

            string FileName = await UploadImageAndMetadata(ImageFile, TimeSync);

            Image Image = new()
            {
                ImageID = Guid.NewGuid(),
                ImageFileName = FileName,
                CreatedAt = TimeSync,
                UpdatedAt = TimeSync
            };

            Image = Mapper.Map(imageDto, Image);

            _context.Images.Add(Image);
            await _context.SaveChangesAsync();

            return Image;
        }

        public async Task<bool> DeleteImageAsync(Guid imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null) return false;

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Image>> GetAllImagesAsync()
        {
            return await _context.Images.ToListAsync();
        }

        public async Task<Image?> GetImageByIdAsync(Guid imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            return image == null ? null : image;
        }

        public async Task<bool> UpdateImageAsync(Guid imageId, ImageDTO imageDto)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null) return false;

            _context.Entry(image).State = EntityState.Modified;

            image.UpdatedAt = DateTime.UtcNow;
            image = Mapper.Map(imageDto, image);
            _context.Update(image);

            await _context.SaveChangesAsync();
            return true;
        }

        public static async Task<string> UploadImageAndMetadata(IFormFile ImageFile, DateTime TimeSync)
        {
            // Để đồng bộ thời gian thêm và cập nhật vì nãy thêm vào dùng 2 cái DateTime.UtcNow nó bị delay một vài milligiây @@
            string UniqueID = new(Enumerable.Repeat(RandomizerChars, 5).Select(Idx => Idx[RNG.Next(Idx.Length)]).ToArray());
            string FileExtension = Path.GetExtension(ImageFile.FileName);
            string FileName = Path.GetFileNameWithoutExtension(ImageFile.FileName) + $"_{DateTimeOffset.Parse(TimeSync.ToString()).ToUnixTimeSeconds()}_{UniqueID}" + FileExtension;

            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "WWWRoot", "Images", FileName);
            using (var Stream = new FileStream(FilePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(Stream);
            }
            return FileName;
        }
    }
}
