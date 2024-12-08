using Application.Data.Models;
using Application.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IOrderDetails
    {
        Task<List<OrderDetail>> GetAll();
        Task<OrderDetail?> GetById(int id);
        Task<OrderDetail> Add(OrderDetailDto orderDetails);
    }
}
