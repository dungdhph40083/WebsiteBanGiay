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
            var productInventoryExists = await _context.Product_Inventorys.AnyAsync(p => p.Inventory_ProductDetailID == productDetailsId);
            if (!productInventoryExists)
            {
                // Nếu không tồn tại, tạo một bản ghi mới trong Product_Inventorys
                var newProductInventory = new Inventory_ProductDetail()
                {
                    Inventory_ProductDetailID = productDetailsId, // Gán ID giả mạo
                                                           // Gán các thuộc tính khác nếu cần
                };

                _context.Product_Inventorys.Add(newProductInventory);
                await _context.SaveChangesAsync(); // Lưu bản ghi mới
            }

            var inventoryLog = new InventoryLog
            {
                LogID = Guid.NewGuid(), // Tạo ID mới
                SizeID = inventoryLogDto.SizeID,
                ColorID = inventoryLogDto.ColorID,
                QuantityInStock = inventoryLogDto.QuantityInStock,
                Status = inventoryLogDto.Status,
                CreatedAt = DateTime.UtcNow,
            };

            _context.InventoryLogs.Add(inventoryLog);
            await _context.SaveChangesAsync();

            return new InventoryLogDto
            {
                LogID = inventoryLog.LogID,
                SizeID = inventoryLog.SizeID,
                ColorID = inventoryLog.ColorID,
                QuantityInStock = inventoryLog.QuantityInStock,
                Status = inventoryLog.Status,
                CreatedAt = inventoryLog.CreatedAt
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
               SizeID = log.SizeID,
               ColorID = log.ColorID,
               QuantityInStock = log.QuantityInStock,
               Status = log.Status,
               CreatedAt = log.CreatedAt
           }).ToListAsync();
        }

        public async Task<InventoryLogDto?> GetInventoryLogByIdAsync(Guid logId)
        {
            var log = await _context.InventoryLogs.FindAsync(logId);
            if (log == null) return null;

            return new InventoryLogDto
            {
                LogID = log.LogID,
                SizeID = log.SizeID,
                ColorID = log.ColorID,
                QuantityInStock = log.QuantityInStock,
                Status = log.Status,
                CreatedAt = log.CreatedAt
            };
        }

        public async Task<bool> UpdateInventoryLogAsync(Guid logId, InventoryLogDto inventoryLogDto)
        {
            var log = await _context.InventoryLogs.FindAsync(logId);
            if (log == null) return false;

            log.SizeID = inventoryLogDto.SizeID;
            log.ColorID = inventoryLogDto.ColorID;
            log.QuantityInStock = inventoryLogDto.QuantityInStock;
            log.Status = inventoryLogDto.Status;
            log.CreatedAt = inventoryLogDto.CreatedAt; // Cập nhật thông tin khác nếu cần

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
