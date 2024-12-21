using Application.Data.DTOs;
using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(OrderDto orderDto);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(Guid orderId);
        Task<Order?> UpdateOrderAsync(Guid ID, OrderDto orderDto);
        Task<bool> DeleteOrderAsync(Guid orderId);
        Task<Order?> UpdateOrderStatus(Guid ID, byte StatusCode);
        Task<Order?> UpdateOrderHasPaid(Guid ID, bool Toggle);
    }
}
