using Application.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IOrderRepository
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(Guid orderId);
        Task<OrderDto> UpdateOrderAsync(OrderDto orderDto);
        Task<bool> DeleteOrderAsync(Guid orderId);
    }
}
