using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IOrderDetails
    {
        IEnumerable<OrderDetail> GetAll();
        OrderDetail GetById(int id);
        void Add(OrderDetail orderDetails);
        void Update(OrderDetail orderDetails);
        void Delete(int id);
        void Save();
    }
}
