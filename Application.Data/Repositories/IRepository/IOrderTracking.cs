using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IOrderTracking
    {
        IEnumerable<OrderTracking> GetAll();
        OrderTracking GetById(Guid id);
        void Add(OrderTracking orderTracking);
        void Update(OrderTracking orderTracking);
        void Delete(int id);
        void Save();
    }
}
