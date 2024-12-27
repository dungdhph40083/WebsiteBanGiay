﻿using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.API.Controllers
{
    [Route("api/[CoNtRoLlEr]")]
    [ApiController]
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
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            var createdOrder = await _orderRepository.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderID }, createdOrder);
        }

        [HttpPatch("Bypass/{id}")] // Debug purposes only
        public async Task<ActionResult<Order>> UpdateOrderBypass(Guid id, [FromBody] OrderDto orderDto)
        {
            var updatedOrder = await _orderRepository.UpdateOrderAsyncBypass(id, orderDto);
            if (updatedOrder == null) return NotFound();
            return updatedOrder;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(Guid id, [FromBody] OrderDto orderDto)
        {
            var updatedOrder = await _orderRepository.UpdateOrderAsync(id, orderDto);
            if (updatedOrder == null)
                return BadRequest("Không thể thay đổi thông tin: đơn hàng đã thay đổi thông tin trước đó, hoặc đơn hàng không tồn tại.");

            if (updatedOrder.Status != (byte)OrderStatus.Created && updatedOrder.Status != (byte)OrderStatus.Processed)
                return BadRequest("Không thể thay đổi thông tin: đơn hàng đã hoặc đang trong quá trình xử lý.");

            return updatedOrder;
        }

        [HttpPatch("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, byte StatusCode)
        {
            var Status = (OrderStatus)StatusCode;
            var updatedOrder = await _orderRepository.UpdateOrderStatus(id, StatusCode);
            if (updatedOrder == null)
            {
                switch (Status)
                {
                    case OrderStatus.Canceled:
                        return BadRequest("Không thể hủy đơn khi đã hủy đơn, đã đổi trả, đơn đang giao hoặc đã hoàn thành đơn!");
                    case OrderStatus.Refunding:
                    case OrderStatus.RefundingAgain:
                        return BadRequest("Không thể hoàn trả đơn vì đã quá hạn, hoặc đơn này đã không được cho phép hoàn trả nữa!");
                    default:
                        return NotFound();
                }
            }
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await OrderDetailsRepository.DeleteOrderDetailsFromOrderID(id);
            var result = await _orderRepository.DeleteOrderAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}