using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
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

        public OrderTrackingRepository(GiayDBContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderTracking> GetAll()
        {
            return _context.OrderTrackings.ToList();
        }

        public OrderTracking GetById(Guid id)
        {
            return _context.OrderTrackings.Find(id);
        }

        public void Add(OrderTracking orderTracking)
        {
            _context.OrderTrackings.Add(orderTracking);
        }

        public void Update(OrderTracking orderTracking)
        {
            _context.OrderTrackings.Update(orderTracking);
        }

        public void Delete(int id)
        {
            var orderTracking = _context.OrderTrackings.Find(id);
            if (orderTracking != null)
            {
                _context.OrderTrackings.Remove(orderTracking);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
