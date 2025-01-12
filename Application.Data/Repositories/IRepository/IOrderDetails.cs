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
        Task<OrderDetail?> GetById(Guid id);
        Task<List<OrderDetail>> GetOrderDetailsFromOrderID(Guid OrderID);
        Task<List<OrderDetail>> GetDetailsByProductDetailID(Guid ID);
        Task DeleteOrderDetailsFromOrderID(Guid OrderID);
        Task DeleteByProductID(Guid DeleteByProductID);
        Task<OrderDetail> Add(OrderDetailDto orderDetails);
        Task<List<OrderDetail>> ImportFromUserCart(Guid UserID, Guid OrderID);
    }
}
