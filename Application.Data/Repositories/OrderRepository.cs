using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
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
                OrderDate = DateTime.UtcNow
            };

            Order = Mapper.Map(orderDto, Order);

            await _context.Orders.AddAsync(Order);
            await _context.SaveChangesAsync();

            return Order;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return default!;

            return order;
        }

        public async Task<Order?> UpdateOrderAsync(Guid ID, OrderDto orderDto)
        {
            var order = await _context.Orders.FindAsync(ID);
            if (order == null) return default!;

            order = Mapper.Map(orderDto, order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
