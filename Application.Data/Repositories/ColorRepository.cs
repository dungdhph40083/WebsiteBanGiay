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
    public class ColorRepository : IColorRepository
    {
        private readonly GiayDBContext _context;

        public ColorRepository(GiayDBContext context)
        {
            _context = context;
        }
        public async Task<Color> CreateColor(ColorDTO colorDTO)
        {
            var color = new Color
            {
                ColorID = Guid.NewGuid(),
                ColorName = colorDTO.ColorName,
                Status = colorDTO.Status,
                CreatedAt = DateTime.UtcNow, 
                UpdatedAt = DateTime.UtcNow
            };

            _context.Colors.Add(color);
            await _context.SaveChangesAsync();

            return color;
        }
        public List<Color?> GetColorsByProductId(Guid productId)
        {
            GiayDBContext _context = new GiayDBContext();
            return _context.ProductDetails.Where(p => p.ProductID == productId).Select(p => p.Color).Distinct()
           .ToList();
        }
        public List<Color?> GetColorsByProductIdAndSize(Guid productId, Guid size)
        {
            GiayDBContext _context = new GiayDBContext();
            return _context.ProductDetails.Where(p => p.ProductID == productId && p.SizeID == size)
                .Select(p => p.Color).Distinct().ToList();
        }
        public async Task DeleteColor(Guid id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color != null)
            {
                _context.Colors.Remove(color);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Color>> GetAllColors()
        {
            return await _context.Colors
           .ToListAsync();
        }

        public async Task<Color?> GetColorById(Guid id)
        {
            return await _context.Colors.FindAsync(id);
        }

        public async Task<Color?> UpdateColor(Guid ID, ColorDTO colorDTO)
        {
            var color = await _context.Colors.FindAsync(ID);
            if (color != null)
            {
                color.ColorName = colorDTO.ColorName;
                color.Status = colorDTO.Status;
                color.UpdatedAt = DateTime.UtcNow;

                _context.Colors.Update(color);
                await _context.SaveChangesAsync();

                return color;
            }
            else return default;
        }

        public async Task<bool> ColorNameAvailibility(string? ColorName)
        {
            var Target = await _context.Colors.FirstOrDefaultAsync(Bruh => Bruh.ColorName == ColorName);
            if (Target == null) return true;
            else return false;
        }
    }
}
