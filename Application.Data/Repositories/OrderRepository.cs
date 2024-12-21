using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        private readonly IMapper Mapper;

        public OrderRepository(GiayDBContext context, IMapper Mapper)
        {
            _context = context;
            this.Mapper = Mapper;

        }

        public async Task<Order> CreateOrderAsync(OrderDto orderDto)
        {
            Order Order = new Order()
            {
                OrderID = Guid.NewGuid(),
                OrderDate = DateTime.UtcNow,
                Status = (int)OrderStatus.Created,
                HasPaid = false
            };

            Order = Mapper.Map(orderDto, Order);

            await _context.Orders.AddAsync(Order);
            await _context.SaveChangesAsync();

            return Order;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(ASD => ASD.User)
                .Include(ASF => ASF.PaymentMethod)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _context.Orders
                .Include(ASD => ASD.User)
                .Include(ASF => ASF.PaymentMethod)
                .SingleOrDefaultAsync(x => x.OrderID == orderId);
            if (order == null) return default!;

            return order;
        }

        public async Task<Order?> UpdateOrderAsync(Guid ID, OrderDto orderDto)
        {
            var order = await GetOrderByIdAsync(ID);
            if (order == null) return default!;

            _context.Orders.Attach(order);
            order = Mapper.Map(orderDto, order);

            _context.Update(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order?> UpdateOrderStatus(Guid ID, byte StatusCode)
        {
            var Order = await GetOrderByIdAsync(ID);
            if (Order == null) return default;

            _context.Orders.Attach(Order);
            Order.Status = StatusCode;
            _context.Update(Order);
            await _context.SaveChangesAsync();

            return Order;
        }

        public async Task<Order?> UpdateOrderHasPaid(Guid ID, bool Toggle)
        {
            var Order = await GetOrderByIdAsync(ID);
            if (Order == null || Order.Status < (int)OrderStatus.Processed) return default;

            _context.Orders.Attach(Order);
            Order.HasPaid = Toggle;
            _context.Update(Order);
            await _context.SaveChangesAsync();

            return Order;
        }
    }
}
