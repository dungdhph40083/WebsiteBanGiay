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
    public class InventoryLogRepository : IInventoryLogRepository
    {
        private readonly GiayDBContext _context;

        public InventoryLogRepository(GiayDBContext context)
        {
            _context = context;
        }
        public async Task<InventoryLogDto> CreateInventoryLogAsync(InventoryLogDto inventoryLogDto)
        {
            var productDetailsId = Guid.NewGuid(); // Tạo ID mới

            // Kiểm tra xem ProductDetailsID có hợp lệ không
            var productInventoryExists = await _context.Product_Inventorys.AnyAsync(p => p.ProductInventoryID == productDetailsId);
            if (!productInventoryExists)
            {
                // Nếu không tồn tại, tạo một bản ghi mới trong Product_Inventorys
                var newProductInventory = new Product_Inventory
                {
                    ProductInventoryID = productDetailsId, // Gán ID giả mạo
                                                           // Gán các thuộc tính khác nếu cần
                };

                _context.Product_Inventorys.Add(newProductInventory);
                await _context.SaveChangesAsync(); // Lưu bản ghi mới
            }

            var inventoryLog = new InventoryLog
            {
                LogID = Guid.NewGuid(), // Tạo ID mới
                ProductDetailsID = productDetailsId, // Sử dụng ID đã tạo
                Size = inventoryLogDto.Size,
                Color = inventoryLogDto.Color,
                QuantityInStock = inventoryLogDto.QuantityInStock,
                Status = inventoryLogDto.Status,
                CreateDateAt = DateTime.UtcNow,
                CreateDateBy = DateTime.UtcNow
            };

            _context.InventoryLogs.Add(inventoryLog);
            await _context.SaveChangesAsync();

            return new InventoryLogDto
            {
                LogID = inventoryLog.LogID,
                ProductDetailsID = inventoryLog.ProductDetailsID,
                Size = inventoryLog.Size,
                Color = inventoryLog.Color,
                QuantityInStock = inventoryLog.QuantityInStock,
                Status = inventoryLog.Status,
                CreateDateAt = inventoryLog.CreateDateAt,
                CreateDateBy = inventoryLog.CreateDateBy
            };
        }

        public async Task<bool> DeleteInventoryLogAsync(Guid logId)
        {
            var log = await _context.InventoryLogs.FindAsync(logId);
            if (log == null) return false;

            _context.InventoryLogs.Remove(log);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InventoryLogDto>> GetAllInventoryLogsAsync()
        {
            return await _context.InventoryLogs
           .Select(log => new InventoryLogDto
           {
               LogID = log.LogID,
               ProductDetailsID = log.ProductDetailsID,
               Size = log.Size,
               Color = log.Color,
               QuantityInStock = log.QuantityInStock,
               Status = log.Status,
               CreateDateAt = log.CreateDateAt,
               CreateDateBy = log.CreateDateBy
           }).ToListAsync();
        }

        public async Task<InventoryLogDto?> GetInventoryLogByIdAsync(Guid logId)
        {
            var log = await _context.InventoryLogs.FindAsync(logId);
            if (log == null) return null;

            return new InventoryLogDto
            {
                LogID = log.LogID,
                ProductDetailsID = log.ProductDetailsID,
                Size = log.Size,
                Color = log.Color,
                QuantityInStock = log.QuantityInStock,
                Status = log.Status,
                CreateDateAt = log.CreateDateAt,
                CreateDateBy = log.CreateDateBy
            };
        }

        public async Task<bool> UpdateInventoryLogAsync(Guid logId, InventoryLogDto inventoryLogDto)
        {
            var log = await _context.InventoryLogs.FindAsync(logId);
            if (log == null) return false;

            log.Size = inventoryLogDto.Size;
            log.Color = inventoryLogDto.Color;
            log.QuantityInStock = inventoryLogDto.QuantityInStock;
            log.Status = inventoryLogDto.Status;
            log.CreateDateAt = inventoryLogDto.CreateDateAt; // Cập nhật thông tin khác nếu cần
            log.CreateDateBy = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
