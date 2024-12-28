using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly GiayDBContext _context;
        private readonly IMapper Mapper;
        const string RandomizerChars = "0123456789";

        public OrderRepository(GiayDBContext context, IMapper Mapper)
        {
            _context = context;
            this.Mapper = Mapper;

        }

        public async Task<Order> CreateOrderAsync(OrderDto orderDto)
        {
            string OrderNumber = OrderNumberGenerator();

            Order Order = new Order()
            {
                OrderID = Guid.NewGuid(),
                OrderNumber = OrderNumber, // no shit sherlock
                OrderDate = DateTime.UtcNow,
                Status = (int)OrderStatus.Created,
                HasPaid = false
            };

            Order = Mapper.Map(orderDto, Order);

            await _context.Orders.AddAsync(Order);
            await _context.SaveChangesAsync();

            return Order;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var LeList = await _context.Orders
                .Include(ASD => ASD.User)
                .Include(ASF => ASF.PaymentMethod)
                .OrderByDescending(DTN => DTN.OrderDate)
                .ToListAsync();
            foreach (var Order in LeList)
            {
                await ConstantUpdates(Order);
            }
            return LeList;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _context.Orders
                .Include(ASD => ASD.User)
                .Include(ASF => ASF.PaymentMethod)
                .OrderByDescending(DTN => DTN.OrderDate)
                .SingleOrDefaultAsync(x => x.OrderID == orderId);
            if (order == null) return default!;
            else await ConstantUpdates(order);

            return order;
        }

        public async Task<List<Order>> GetOrdersByUserID(Guid UserID)
        {
            var Orders = await _context.Orders
                .Include(ASD => ASD.User)
                .Include (ASF => ASF.PaymentMethod)
                .OrderByDescending(DTN => DTN.OrderDate)
                .Where(x => x.UserID == UserID)
            .ToListAsync();

            foreach (var Order in Orders)
            {
                await ConstantUpdates(Order);
            }

            return Orders;
        }

        public async Task<Order?> UpdateOrderAsyncBypass(Guid ID, OrderDto orderDto)
        {
            var order = await GetOrderByIdAsync(ID);
            if (order == null) return default!;

            _context.Orders.Attach(order);
            order = Mapper.Map(orderDto, order);

            _context.Update(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order?> UpdateOrderAsync(Guid ID, OrderDto orderDto)
        {
            var order = await GetOrderByIdAsync(ID);
            if (order == null || order.HasChangedInfo) return default;

            if ((order.Status != (byte)OrderStatus.Created && order.Status != (byte)OrderStatus.Processed)) return order;
            else
            {
                _context.Orders.Attach(order);

                order.HasChangedInfo = true;

                order = Mapper.Map(orderDto, order);

                _context.Update(order);
                await _context.SaveChangesAsync();

                Console.ForegroundColor = (ConsoleColor)Random.Shared.Next(0, 16);
                Console.WriteLine(order.HasChangedInfo);

                return order;
            }
        }

        public async Task<Order?> UpdateOrderStatus(Guid ID, byte StatusCode)
        {
            // Up next: Test the API, then integrate to the 

            var DateTimeUtcNow = DateTime.UtcNow;
            var Status = (OrderStatus)StatusCode;

            var Order = await GetOrderByIdAsync(ID);
            if (Order == null) return default;
            if (Order.Status == StatusCode) return Order;

            _context.Orders.Attach(Order);

            // Xem class "OrderStatus" ở "EnumsTable.cs" để biết thêm
            switch (Status)
            {
                /***/
                case OrderStatus.Canceled:
                    {
                        if (Order.Status == (byte)OrderStatus.Created)
                        {
                            Order.Status = StatusCode;

                            _context.Update(Order);
                            await _context.SaveChangesAsync();
                            return Order;
                        }
                        else return default;
                    }
                /***/
                case OrderStatus.Refunding:
                case OrderStatus.RefundingAgain:
                    {
                        if (
                            (Order.Status == (byte)OrderStatus.Received || Order.Status == (byte)OrderStatus.ReceivedAgain) &&
                             DateTimeUtcNow >= Order.RefundStart && DateTimeUtcNow <= Order.RefundEnd
                            )
                        {
                            Order.Status = StatusCode;

                            _context.Update(Order);
                            await _context.SaveChangesAsync();
                            return Order;
                        }
                        else return default;
                    }
                /***/
                case OrderStatus.Received:
                    {
                        if (Order.RefundStart == null) Order.RefundStart = DateTimeUtcNow;
                        if (Order.RefundEnd == null) Order.RefundEnd = DateTimeUtcNow.AddDays(7);

                        Order.Status = StatusCode;

                        Order.HasPaid = true;
                        _context.Update(Order);
                        await _context.SaveChangesAsync();
                        return Order;
                    }
                case OrderStatus.Arrived:
                    {
                        if (Order.AcceptStart == null) Order.AcceptStart = DateTimeUtcNow;
                        if (Order.AcceptEnd == null) Order.AcceptEnd = DateTimeUtcNow.AddDays(2);

                        Order.Status = StatusCode;

                        _context.Update(Order);
                        await _context.SaveChangesAsync();
                        return Order;
                    }
                /***/
                // Nếu như thanh toán trước bằng VNPay hoặc MoMo các thứ thì cho thêm logic vào phần "Created".
                case OrderStatus.RefundProcessed:
                case OrderStatus.RefundDelivered:
                case OrderStatus.Refunded:
                case OrderStatus.Created:
                case OrderStatus.Processed:
                case OrderStatus.Delivered:
                case OrderStatus.ReceivedAgain:
                case OrderStatus.ReceivedCompleted:
                    {
                        Order.Status = StatusCode;

                        _context.Update(Order);
                        await _context.SaveChangesAsync();
                        return Order;
                    }
                /***/
                default:
                    return default;
            }
        }

        public async Task<Order?> UpdateOrderHasPaid(Guid ID, bool Toggle)
        {
            var Order = await GetOrderByIdAsync(ID);
            if (Order == null || Order.Status < (byte)OrderStatus.Processed) return default;

            _context.Orders.Attach(Order);
            Order.HasPaid = Toggle;
            _context.Update(Order);
            await _context.SaveChangesAsync();

            return Order;
        }

        private static string OrderNumberGenerator()
        {
            string UniqueID = new(Enumerable.Repeat(RandomizerChars, 5).Select(Idx => Idx[Random.Shared.Next(Idx.Length)]).ToArray());

            string OrderNumber = $"HD_{DateTime.UtcNow:ddMMyyyy_HHmmss}_{UniqueID}";
            return OrderNumber;
        }

        private async Task ConstantUpdates(Order Order)
        {
            if (Order != null)
            {
                if (Order.Status == (int)OrderStatus.Arrived && DateTime.UtcNow > Order.AcceptEnd)
                {
                    _context.Orders.Attach(Order);
                    Order.Status = (int)OrderStatus.Received;

                    _context.Update(Order);
                    await _context.SaveChangesAsync();
                }
                if ((Order.Status == (int)OrderStatus.Received || Order.Status == (int)OrderStatus.ReceivedAgain)
                    && DateTime.UtcNow > Order.RefundEnd)
                {
                    _context.Orders.Attach(Order);
                    Order.Status = (int)OrderStatus.ReceivedCompleted;

                    _context.Update(Order);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}