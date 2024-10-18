
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
	public class OrderDetailsRepository : IOrderDetails
    {
        private readonly GiayDBContext _context;

        public OrderDetailsRepository(GiayDBContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _context.OrderDetails.ToList();
        }

        public OrderDetail GetById(int id)
        {
            return _context.OrderDetails.Find(id);
        }

        public void Add(OrderDetail orderDetails)
        {
            _context.OrderDetails.Add(orderDetails);
        }

        public void Update(OrderDetail orderDetails)
        {
            _context.OrderDetails.Update(orderDetails);
        }

        public void Delete(int id)
        {
            var orderDetails = _context.OrderDetails.Find(id);
            if (orderDetails != null)
            {
                _context.OrderDetails.Remove(orderDetails);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
