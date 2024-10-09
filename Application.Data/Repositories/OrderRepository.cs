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
    public class OrderRepository:IOrderRepository
    {
        private readonly GiayDBContext _context;

        public OrderRepository(GiayDBContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            var userId = Guid.NewGuid(); // Tạo ID giả mạo cho User
            var paymentMethodId = Guid.NewGuid(); // Tạo ID giả mạo cho Payment Method

            // Kiểm tra xem UserID có tồn tại không, nếu không thì tạo mới
            if (!await _context.Users.AnyAsync(u => u.UserID == userId))
            {
                var newUser = new User
                {
                    UserID = userId,
                    Username = "fakeUser", // Tạo tên người dùng giả
                    Password = "fakePassword" // Tạo mật khẩu giả
                };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
            }

            // Kiểm tra xem PaymentMethodID có tồn tại không, nếu không thì tạo mới
            if (!await _context.PaymentMethods.AnyAsync(p => p.PaymentMethodID == paymentMethodId))
            {
                var newPaymentMethod = new PaymentMethod
                {
                    PaymentMethodID = paymentMethodId,
                    MethodName = "fakeMethod" // Tạo phương thức thanh toán giả
                };
                _context.PaymentMethods.Add(newPaymentMethod);
                await _context.SaveChangesAsync();
            }

            var order = new Order
            {
                OrderID = Guid.NewGuid(),
                UserID = userId,
                OrderDate = DateTime.UtcNow,
                Status = orderDto.Status,
                TotalPrice = orderDto.TotalPrice,
                ShippingAddress = orderDto.ShippingAddress,
                PaymentMethodID = paymentMethodId
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new OrderDto
            {
                OrderID = order.OrderID,
                UserID = order.UserID,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                ShippingAddress = order.ShippingAddress,
                PaymentMethodID = order.PaymentMethodID
            };
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            return await _context.Orders.Select(order => new OrderDto
            {
                OrderID = order.OrderID,
                UserID = order.UserID,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                ShippingAddress = order.ShippingAddress,
                PaymentMethodID = order.PaymentMethodID
            }).ToListAsync();
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return default!;

            return new OrderDto
            {
                OrderID = order.OrderID,
                UserID = order.UserID,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                ShippingAddress = order.ShippingAddress,
                PaymentMethodID = order.PaymentMethodID
            };
        }

        public async Task<OrderDto> UpdateOrderAsync(OrderDto orderDto)
        {
            var order = await _context.Orders.FindAsync(orderDto.OrderID);
            if (order == null) return default!;

            order.Status = orderDto.Status;
            order.TotalPrice = orderDto.TotalPrice;
            order.ShippingAddress = orderDto.ShippingAddress;

            await _context.SaveChangesAsync();

            return orderDto;
        }
    }
}
