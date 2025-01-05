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
                AttemptsLeft = 3,
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
                case OrderStatus.DeliveryFailure:
                    {
                        // Giao hàng thất bại: trừ 1 lần thử và cho phép tạo lại
                        // Nếu còn 0 lần thử mà vẫn thất bại = hỏng hẳn
                        if (Order.AttemptsLeft > 0) { Order.AttemptsLeft -= 1; Order.Status = StatusCode; }
                        else Order.Status = (byte)OrderStatus.DeliveryIsDead;

                        Order.AcceptStart = null;
                        Order.AcceptEnd = null;
                        Order.RefundStart = null;
                        Order.RefundEnd = null;

                        _context.Update(Order);
                        await _context.SaveChangesAsync();
                        return Order;
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
                case OrderStatus.Processed:
                    {
                        // NOTE TO SELF: MAKE THE DEDUCT ITEMS FROM THE PRODUCT REPOSITORY, THEN NET IT INTO THE API

                        if (Order.Status == (byte)OrderStatus.DeliveryIsDead) return default;

                        Order.Status = StatusCode;

                        _context.Update(Order);

                        await _context.SaveChangesAsync();
                        return Order;
                    }
                /***/
                // Nếu như thanh toán trước bằng VNPay hoặc MoMo các thứ thì cho thêm logic vào phần "Created".
                case OrderStatus.RefundProcessed:
                case OrderStatus.RefundDelivered:
                case OrderStatus.RefundReceived:
                case OrderStatus.Refunded:
                case OrderStatus.DeliveryIsDead:
                case OrderStatus.Created:
                case OrderStatus.Delivered:
                case OrderStatus.ReceivedAgain:
                case OrderStatus.ReceivedCompleted:
                case OrderStatus.ReceivedRefundFail:
                    {
                        // rare 50c
                        // rare 50c

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

        public async Task<List<Order>> GetOrdersByFilter(string SearchFilter)
        {
            var LeList = new List<Order>();
            switch (SearchFilter)
            {
                case OrderFilters.ORDERS_PENDING:
                    {
                        LeList = await _context.Orders
                            .Include(ASD => ASD.User)
                            .Include(ASF => ASF.PaymentMethod)
                            .OrderByDescending(DTN => DTN.OrderDate)
                            .OrderByDescending(DTS => DTS.Status)
                            .Where(FLT => FLT.Status == (byte)OrderStatus.Created)
                            .ToListAsync();
                        break;
                    }
                case OrderFilters.ORDERS_ONGOING:
                    {
                        LeList = await _context.Orders
                            .Include(ASD => ASD.User)
                            .Include(ASF => ASF.PaymentMethod)
                            .OrderByDescending(DTN => DTN.OrderDate)
                            .OrderByDescending(DTS => DTS.Status)
                            .Where(FLT => FLT.Status == (byte)OrderStatus.Processed || FLT.Status == (byte)OrderStatus.Delivered || FLT.Status == (byte)OrderStatus.Arrived)
                            .ToListAsync();
                        break;
                    }
                case OrderFilters.ORDERS_SUCCEEDED:
                    {
                        LeList = await _context.Orders
                            .Include(ASD => ASD.User)
                            .Include(ASF => ASF.PaymentMethod)
                            .OrderByDescending(DTN => DTN.OrderDate)
                            .OrderByDescending(DTS => DTS.Status)
                            .Where(FLT => FLT.Status == (byte)OrderStatus.Received || FLT.Status == (byte)OrderStatus.ReceivedAgain || FLT.Status == (byte)OrderStatus.ReceivedCompleted)
                            .ToListAsync();
                        break;
                    }
                case OrderFilters.ORDERS_FAILED:
                    {
                        LeList = await _context.Orders
                            .Include(ASD => ASD.User)
                            .Include(ASF => ASF.PaymentMethod)
                            .OrderByDescending(DTN => DTN.OrderDate)
                            .OrderByDescending(DTS => DTS.Status)
                            .Where(FLT => FLT.Status == (byte)OrderStatus.DeliveryIsDead || FLT.Status == (byte)OrderStatus.DeliveryFailure)
                            .ToListAsync();
                        break;
                    }
                default:
                    break;
            }
            if (LeList.Count > 0)
            {
                foreach (var Order in LeList)
                {
                    await ConstantUpdates(Order);
                }
            }
            return LeList;
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
        public async Task<Order> GetOrderByOrderNumberAsync(string orderNumber)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }
    }
}