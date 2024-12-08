
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
	public class OrderDetailsRepository : IOrderDetails
    {
        private readonly GiayDBContext _context;
        private readonly IMapper Mapper;

        public OrderDetailsRepository(GiayDBContext context, IMapper Mapper)
        {
            _context = context;
            this.Mapper = Mapper;
        }

        public async Task<List<OrderDetail>> GetAll()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail?> GetById(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        }

        public async Task<OrderDetail> Add(OrderDetailDto orderDetails)
        {
            OrderDetail OrderDetail = new()
            {
                OrderDetailID = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
            };

            OrderDetail = Mapper.Map(orderDetails, OrderDetail);

            await _context.OrderDetails.AddAsync(OrderDetail);
            await _context.SaveChangesAsync();

            return OrderDetail;
        }
    }
}
