using Application.Data.DTOs;
using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IColorRepository
    {
        Task<List<Color>> GetAllColors();
        List<Color?> GetColorsByProductId(Guid productId);
        List<Color?> GetColorsByProductIdAndSize(Guid productId, Guid size);
        Task<Color?> GetColorById(Guid id);
        Task<Color> CreateColor(ColorDTO colorDTO);
        Task<Color?> UpdateColor(Guid ID, ColorDTO colorDTO);
        Task DeleteColor(Guid id);
    }
}
