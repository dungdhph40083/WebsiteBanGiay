﻿using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Aspose.Pdf.AI;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly GiayDBContext _context;
        private readonly IMapper Mapper;
        const string RandomizerChars = "0123456789abcdef";

        public ImageRepository(GiayDBContext context, IMapper Mapper)
        {
            _context = context;
            this.Mapper = Mapper;
        }
        public async Task<Image> CreateImageAsync(IFormFile ImageFile)
        {
            DateTime TimeSync = DateTime.UtcNow;

            string FileName = await UploadImageAndMetadata(ImageFile, TimeSync);

            Image Image = new()
            {
                ImageID = Guid.NewGuid(),
                ImageFileName = FileName,
            };

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

        public async Task<List<Image>> GetAllImagesAsync()
        {
            return await _context.Images.OrderBy(B => B.ImageFileName).ToListAsync();
        }

        public async Task<Image?> GetImageByIdAsync(Guid imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            return image == null ? null : image;
        }

        public static async Task<string> UploadImageAndMetadata(IFormFile ImageFile, DateTime TimeSync)
        {
            // Để đồng bộ thời gian thêm và cập nhật vì nãy thêm vào dùng 2 cái DateTime.UtcNow nó bị delay một vài milligiây @@
            string UniqueID = new(Enumerable.Repeat(RandomizerChars, 5).Select(Idx => Idx[Random.Shared.Next(Idx.Length)]).ToArray());
            string FileExtension = Path.GetExtension(ImageFile.FileName);
            string FileName = Path.GetFileNameWithoutExtension(ImageFile.FileName) + $"_{DateTimeOffset.Parse(TimeSync.ToString()):dd-MM-yyyy_HH-mm-ss}_{UniqueID}" + FileExtension;

            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "WWWRoot", "Images", FileName);
            using (var Stream = new FileStream(FilePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(Stream);
            }
            return FileName;
        }

        /*
         * [15/1/2025]
         * 
         * Let me explain why this is an actual addition to this project.
         * At the time of writing this, we're only a single day until we
         * have to be present at college for our final presentation.
         * 
         * And since we decide to try to grab the most score possible in this
         * time frame alone (1 day), we decided to add this 'last-minute-magic'
         * ass addition that is to have a product display multiple images at same
         * time. And after coming up for a strategy to implement this BS,
         * we decided to do the lazy way out instead of creating a 
         * N-N relational table for Products-Images relations like a normal
         * person would do logically.
         * 
         * Don't worry, I cringed at this too. It's not your fault. It's mine's.
         * But we had no other choice...
         */

        public async Task<List<Image>> CreateShittonOfImagesWithProductIDAsync(Guid? ProductID, List<IFormFile> Files)
        {
            DateTime TimeSync = DateTime.UtcNow;

            var ImageList = new List<Image>();

            foreach (var Thing in Files)
            {
                string FileName = await UploadImageAndMetadata(Thing, TimeSync);

                Image Image = new()
                {
                    ImageID = Guid.NewGuid(),
                    ImageFileName = FileName
                };
                if (ProductID != null) Image.ProductID = ProductID;

                ImageList.Add(Image);
            }

            await _context.Images.AddRangeAsync(ImageList);
            await _context.SaveChangesAsync();

            return ImageList;
        }

        public async Task<List<Image>> GetImagesByProductID(Guid ProductID)
        {
            return await _context.Images.Where(SOS => SOS.ProductID == ProductID).OrderBy(B => B.ImageFileName).ToListAsync();
        }
    }
}
