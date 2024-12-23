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
    public class OrderTrackingRepository : IOrderTracking
    {
        private readonly GiayDBContext _context;
        private readonly IMapper Mapper;

        public OrderTrackingRepository(GiayDBContext context, IMapper Mapper)
        {
            _context = context;
            this.Mapper = Mapper;
        }

        public async Task<List<OrderTracking>> GetAll()
        {
            return await _context.OrderTrackings
                .Include(Chicken => Chicken.Order)
                .ToListAsync();
        }

        public async Task<List<OrderTracking>> GetStatusesByOrderID(Guid ID)
        {
            return await _context.OrderTrackings
                .Include(Doubt => Doubt.Order)
                .Where(SniffSniff => SniffSniff.OrderID == ID)
                .ToListAsync();
        }

        public async Task<OrderTracking?> GetById(Guid id)
        {
            return await _context.OrderTrackings
                .Include(Chicken => Chicken.Order)
                .SingleOrDefaultAsync(x => x.OrderID == id);
        }

        public async Task<OrderTracking> Add(OrderTrackingDTO orderTracking)
        {
            OrderTracking OrderTracking = new() { TrackingID = Guid.NewGuid(), LogDate = DateTime.UtcNow};
            OrderTracking = Mapper.Map(orderTracking, OrderTracking);

            await _context.OrderTrackings.AddAsync(OrderTracking);
            await _context.SaveChangesAsync();

            return OrderTracking;
        }
    }
}
