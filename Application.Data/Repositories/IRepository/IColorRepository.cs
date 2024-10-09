using Application.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IColorRepository
    {
        Task<IEnumerable<ColorDTO>> GetAllColors();
        Task<ColorDTO> GetColorById(Guid id);
        Task<ColorDTO> CreateColor(ColorDTO colorDTO);
        Task UpdateColor(ColorDTO colorDTO);
        Task DeleteColor(Guid id);
    }
}
