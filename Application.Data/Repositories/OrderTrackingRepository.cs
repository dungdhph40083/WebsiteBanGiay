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
            return await _context.OrderTrackings.ToListAsync();
        }

        public async Task<OrderTracking?> GetById(Guid id)
        {
            return await _context.OrderTrackings.FindAsync(id);
        }

        public async Task<OrderTracking> Add(OrderTrackingDTO orderTracking)
        {
            OrderTracking OrderTracking = new() { TrackingID = Guid.NewGuid()};
            OrderTracking = Mapper.Map(orderTracking, OrderTracking);

            await _context.OrderTrackings.AddAsync(OrderTracking);
            await _context.SaveChangesAsync();

            return OrderTracking;
        }

        public async Task<OrderTracking?> Update(Guid ID, OrderTrackingDTO orderTracking)
        {
            var Target = await GetById(ID);
            if (Target != null)
            {
                _context.Entry(Target).State = EntityState.Modified;
                Target = Mapper.Map(orderTracking, Target);

                _context.Update(Target);
                await _context.SaveChangesAsync();
                return Target;
            }
            else return default;
        }

        public async Task Delete(Guid ID)
        {
            var Target = _context.OrderTrackings.Find(ID);
            if (Target != null)
            {
                _context.Entry(Target).State = EntityState.Modified;
                Target.ShippingStatus = 0;

                _context.Update(Target);
                await _context.SaveChangesAsync();
            }
        }
    }
}
