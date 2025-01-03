using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetails OrderDetailsRepository;
        private readonly IOrderTracking OrderTrackingRepo;

        public OrdersController(IOrderRepository orderRepository, IOrderTracking OrderTrackingRepo, IOrderDetails OrderDetailsRepository)
        {
            _orderRepository = orderRepository;
            this.OrderTrackingRepo = OrderTrackingRepo;
            this.OrderDetailsRepository = OrderDetailsRepository;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }
        [HttpGet("User/{ID}")]
        public async Task<IActionResult> GetOrdersByUserID(Guid ID)
        {
            var orders = await _orderRepository.GetOrdersByUserID(ID);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            var createdOrder = await _orderRepository.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderID }, createdOrder);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderDto orderDto)
        {
            var updatedOrder = await _orderRepository.UpdateOrderAsync(id, orderDto);
            if (updatedOrder == null) return NotFound();
            return (IActionResult)updatedOrder;
        }

        [HttpPatch("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, byte StatusCode)
        {
            var updatedOrder = await _orderRepository.UpdateOrderStatus(id, StatusCode);
            if (updatedOrder == null) return NotFound();
            else
            {
                OrderTrackingDTO NewRecord = new()
                {
                    OrderID = updatedOrder.OrderID,
                    Status = StatusCode,
                    HasPaid = updatedOrder.HasPaid,
                };
                await OrderTrackingRepo.Add(NewRecord);
                return NoContent();
            }
        }
        
        [HttpPatch("UpdateStatusPaid/{id}")]
        public async Task<IActionResult> UpdateOrderHasPaid(Guid id, bool Toggle)
        {
            var updatedOrder = await _orderRepository.UpdateOrderHasPaid(id, Toggle);
            if (updatedOrder == null || updatedOrder.HasPaid == Toggle) return NotFound();
            else
            {
                OrderTrackingDTO NewRecord = new()
                {
                    OrderID = updatedOrder.OrderID,
                    Status = updatedOrder.Status,
                    HasPaid = Toggle
                };
                await OrderTrackingRepo.Add(NewRecord);
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await OrderDetailsRepository.DeleteOrderDetailsFromOrderID(id);
            var result = await _orderRepository.DeleteOrderAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}