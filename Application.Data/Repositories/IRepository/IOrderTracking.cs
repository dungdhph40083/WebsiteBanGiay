using Application.Data.DTOs;
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
        Task<List<OrderTracking>> GetAll();
        Task<OrderTracking?> GetById(Guid id);
        Task<List<OrderTracking>> GetStatusesByOrderID(Guid ID);
        Task<OrderTracking> Add(OrderTrackingDTO orderTracking);
    }
}
