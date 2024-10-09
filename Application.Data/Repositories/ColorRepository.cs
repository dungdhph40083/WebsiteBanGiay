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
        public async Task<ColorDTO> CreateColor(ColorDTO colorDTO)
        {
            var color = new Color
            {
                ColorID = Guid.NewGuid(),
                ColorName = colorDTO.ColorName,
                Status = colorDTO.Status,
                CreatedAt = DateTime.Now.Date,
                UpdatedAt = DateTime.Now.Date
            };

            _context.Colors.Add(color);
            await _context.SaveChangesAsync();

            return colorDTO;
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

        public async Task<IEnumerable<ColorDTO>> GetAllColors()
        {
            return await _context.Colors
           .Select(c => new ColorDTO
           {
               ColorID = c.ColorID,
               ColorName = c.ColorName,
               Status = c.Status,
               CreatedAt = c.CreatedAt,
               UpdateAt = c.UpdatedAt
           })
           .ToListAsync();
        }

        public async Task<ColorDTO> GetColorById(Guid id)
        {
            var color = await _context.Colors.FindAsync(id);
            if (color == null) return null;

            return new ColorDTO
            {
                ColorID = color.ColorID,
                ColorName = color.ColorName,
                Status = color.Status,
                CreatedAt = color.CreatedAt,
                UpdatedAt = color.UpdatedAt
            };
        }

        public async Task UpdateColor(ColorDTO colorDTO)
        {
            var color = await _context.Colors.FindAsync(colorDTO.ColorID);
            if (color != null)
            {
                color.ColorName = colorDTO.ColorName;
                color.Status = colorDTO.Status;
                color.UpdatedAt = DateTime.Now.Date;

                _context.Colors.Update(color);
                await _context.SaveChangesAsync();
            }
        }
    }
}
