using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
	public class OrderDetailsRepository : IOrderDetails
    {
        private readonly GiayDBContext _context;
        private readonly IMapper Mapper;

        public OrderDetailsRepository(GiayDBContext context, IMapper Mapper)
        {
            _context = context;
            this.Mapper = Mapper;
        }

        public async Task<List<OrderDetail>> GetAll()
        {
            return await _context.OrderDetails
                .Include(Pikachu => Pikachu.Voucher)
                    .ThenInclude(Charizard => Charizard != null ? Charizard.Product : null)
                .Include(Pikachu => Pikachu.Voucher)
                    .ThenInclude(Charizard => Charizard != null ? Charizard.Category : null)

                .Include(Pikachu => Pikachu.Sale)
                    .ThenInclude(Sceptile => Sceptile != null ? Sceptile.Product : null)
                .Include(Pikachu => Pikachu.Sale)
                    .ThenInclude(Sceptile => Sceptile != null ? Sceptile.Category : null)

                .Include(Pikachu => Pikachu.Order)

                .Include(Pikachu => Pikachu.ProductDetail)
                    .ThenInclude(Buizel => Buizel != null ? Buizel.Product : null)
                        .ThenInclude(Cinderace => Cinderace != null ? Cinderace.Image : null)
                .Include(Pikachu => Pikachu.ProductDetail)
                    .ThenInclude(Buizel => Buizel != null ? Buizel.Category : null)
                .Include(Pikachu => Pikachu.ProductDetail)
                    .ThenInclude(Buizel => Buizel != null ? Buizel.Color : null)
                .Include(Pikachu => Pikachu.ProductDetail)
                    .ThenInclude(Buizel => Buizel != null ? Buizel.Size : null)

                .Include(Pikachu => Pikachu.ShippingMethod)
                    .ToListAsync();
        }

        public async Task<OrderDetail?> GetById(Guid id)
        {
            return await _context.OrderDetails
                .Include(Pikachu => Pikachu.Voucher)
                    .ThenInclude(Charizard => Charizard != null ? Charizard.Product : null)
                .Include(Pikachu => Pikachu.Voucher)
                    .ThenInclude(Charizard => Charizard != null ? Charizard.Category : null)

                .Include(Pikachu => Pikachu.Sale)
                    .ThenInclude(Sceptile => Sceptile != null ? Sceptile.Product : null)
                .Include(Pikachu => Pikachu.Sale)
                    .ThenInclude(Sceptile => Sceptile != null ? Sceptile.Category : null)

                .Include(Pikachu => Pikachu.Order)

                .Include(Pikachu => Pikachu.ProductDetail)
                    .ThenInclude(Buizel => Buizel != null ? Buizel.Product : null)
                        .ThenInclude(Cinderace => Cinderace != null ? Cinderace.Image : null)
                .Include(Pikachu => Pikachu.ProductDetail)
                    .ThenInclude(Buizel => Buizel != null ? Buizel.Category : null)
                .Include(Pikachu => Pikachu.ProductDetail)
                    .ThenInclude(Buizel => Buizel != null ? Buizel.Color : null)
                .Include(Pikachu => Pikachu.ProductDetail)
                    .ThenInclude(Buizel => Buizel != null ? Buizel.Size : null)

                .Include(Pikachu => Pikachu.ShippingMethod)
                    .SingleOrDefaultAsync(Crayfish => Crayfish.OrderDetailID == id);
        }

        public async Task DeleteOrderDetailsFromOrderID(Guid OrderID)
        {
            var Targets = await _context.OrderDetails
                        .Where(Prdc => Prdc.OrderID == OrderID)
                            .ToListAsync();

            _context.OrderDetails.RemoveRange(Targets);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDetail>> GetOrderDetailsFromOrderID(Guid OrderID)
        {
            return await _context.OrderDetails
                    .Include(Pikachu => Pikachu.Voucher)
                        .ThenInclude(Charizard => Charizard != null ? Charizard.Product : null)
                    .Include(Pikachu => Pikachu.Voucher)
                        .ThenInclude(Charizard => Charizard != null ? Charizard.Category : null)

                    .Include(Pikachu => Pikachu.Sale)
                        .ThenInclude(Sceptile => Sceptile != null ? Sceptile.Product : null)
                    .Include(Pikachu => Pikachu.Sale)
                        .ThenInclude(Sceptile => Sceptile != null ? Sceptile.Category : null)

                    .Include(Pikachu => Pikachu.Order)

                    .Include(Pikachu => Pikachu.ProductDetail)
                        .ThenInclude(Buizel => Buizel != null ? Buizel.Product : null)
                            .ThenInclude(Cinderace => Cinderace != null ? Cinderace.Image : null)
                    .Include(Pikachu => Pikachu.ProductDetail)
                        .ThenInclude(Buizel => Buizel != null ? Buizel.Category : null)
                    .Include(Pikachu => Pikachu.ProductDetail)
                        .ThenInclude(Buizel => Buizel != null ? Buizel.Color : null)
                    .Include(Pikachu => Pikachu.ProductDetail)
                        .ThenInclude(Buizel => Buizel != null ? Buizel.Size : null)

                    .Include(Pikachu => Pikachu.ShippingMethod)
                        .Where(Prdc => Prdc.OrderID == OrderID)
                            .ToListAsync();
        }

        public async Task<OrderDetail> Add(OrderDetailDto orderDetails)
        {
            OrderDetail OrderDetail = new()
            {
                OrderDetailID = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
            };

            OrderDetail = Mapper.Map(orderDetails, OrderDetail);

            await _context.OrderDetails.AddAsync(OrderDetail);
            await _context.SaveChangesAsync();

            return OrderDetail;
        }

        public async Task<List<OrderDetail>> ImportFromUserCart(Guid UserID, Guid OrderID)
        {
            var GetOrder = await _context.Orders.FindAsync(OrderID);
            if (GetOrder == null) return new();

            var MyShoppingCart = await _context.ShoppingCarts
                //.Include(UU => UU.User)
                .Include(UU => UU.Voucher)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Category : null)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Color : null)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Size : null)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : null)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : null)
                        .ThenInclude(WW => WW != null ? WW.Image : null)
                  .Where(UU => UU.UserID.Equals(UserID)).ToListAsync();

            if (MyShoppingCart != null)
            {
                var MyOrders = new List<OrderDetail>();
                foreach (var CartItem in MyShoppingCart)
                {
                    OrderDetail NewDetail = new()
                    {
                        OrderDetailID = Guid.NewGuid(),
                        OrderID = OrderID,
                        ProductDetailID = CartItem.ProductDetailID,
                        ShippingMethodID = null,
                        VoucherID = CartItem.VoucherID,
                        Quantity = CartItem.QuantityCart,
                        Price = CartItem.ProductDetail?.Product?.Price,
                        TotalUnitPrice = CartItem.ProductDetail?.Product?.Price * CartItem.QuantityCart,
                        SumTotalPrice = CartItem.ProductDetail?.Product?.Price * CartItem.QuantityCart - (CartItem.Voucher?.DiscountPrice ?? 0), // Tương tự...
                        CreatedAt = DateTime.UtcNow
                    };
                    MyOrders.Add(NewDetail);
                }
                await _context.OrderDetails.AddRangeAsync(MyOrders);
                await _context.SaveChangesAsync();

                _context.Orders.Attach(GetOrder);

                GetOrder.GrandTotal = MyOrders.Sum(S => S.SumTotalPrice);

                _context.Update(GetOrder);
                await _context.SaveChangesAsync();

                return MyOrders;
            }
            else return new();
        }
    }
}
