using Application.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IInventoryLogRepository
    {
        Task<IEnumerable<InventoryLogDto>> GetAllInventoryLogsAsync();
        Task<InventoryLogDto?> GetInventoryLogByIdAsync(Guid logId);
        Task<InventoryLogDto> CreateInventoryLogAsync(InventoryLogDto inventoryLogDto);
        Task<bool> UpdateInventoryLogAsync(Guid logId, InventoryLogDto inventoryLogDto);
        Task<bool> DeleteInventoryLogAsync(Guid logId);
    }
}
