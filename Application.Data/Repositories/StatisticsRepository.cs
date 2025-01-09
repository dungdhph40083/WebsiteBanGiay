﻿using Application.Data.ModelContexts;
using Application.Data.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly GiayDBContext _context;

        public StatisticsRepository(GiayDBContext context)
        {
            _context = context;
        }
        //public async Task<int> CountOrdersByStatusAndDateAsync(byte status, DateTime startDate, DateTime endDate)
        //{
        //    // Thiết lập mặc định thời gian cho startDate và endDate
        //    startDate = startDate.Date; // Lấy ngày với giờ là 00:00:00
        //    endDate = endDate.Date.AddDays(1).AddMilliseconds(-1); // Lấy ngày tiếp theo của endDate và giảm đi 1 mili giây (23:59:59)

        //    // Truy vấn với ngày bắt đầu và kết thúc đã được điều chỉnh
        //    return await _context.Orders
        //        .Where(o => o.Status == status && o.OrderDate >= startDate && o.OrderDate <= endDate)
        //        .CountAsync();
        //}

        public async Task<int> CountOrdersByStatusesAndDateAsync(byte[] statuses, DateTime startDate, DateTime endDate)
        {
            return await _context.Orders
                .Where(o => statuses.Contains(o.Status) && o.OrderDate >= startDate && o.OrderDate <= endDate)
                .CountAsync();
        }
        public async Task<long?> GetTotalAmountByStatusesAndDateAsync(byte[] statuses, DateTime startDate, DateTime endDate)
        {
            return await _context.Orders
                .Where(o => statuses.Contains(o.Status) && o.OrderDate >= startDate && o.OrderDate <= endDate)
                .SumAsync(o => o.GrandTotal);  // Giả sử "GrandTotal" là cột tổng tiền của đơn hàng
        }
        public async Task<int> GetTotalProductsAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<int> GetTotalCategoriesAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetBannedAccountsAsync()
        {
            // Chuyển chuỗi GUID thành Guid object
            Guid roleIdToCheck = Guid.Parse("B463986E-60F1-4029-91C4-1116C4E073E7");

            // Đếm số tài khoản có RoleID khớp
            return await _context.Users
                .Where(u => u.RoleID == roleIdToCheck) // Điều kiện chỉ kiểm tra RoleID
                .CountAsync();
        }

        public async Task<int> GetTotalVouchersAsync()
        {
            return await _context.Vouchers.CountAsync();
        }
        public async Task<int> GetTotalContactsAsync()
        {
            return await _context.CustomerSupportMessages.CountAsync();
        }

    }
}
