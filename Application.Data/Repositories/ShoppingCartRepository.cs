using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace Application.Data.Repositories
{
    public class ShoppingCartRepository : IShoppingCart
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;

        public ShoppingCartRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        // you know what f this i'm just gonna implement a singular class and call it a day because i'm SO tired
        public async Task<ShoppingCart> CreateNew(ShoppingCartDTO NewShoppingCart)
        {
            ShoppingCart ShoppingCart = new() { CartID = Guid.NewGuid() };

            ShoppingCart = Mapper.Map(NewShoppingCart, ShoppingCart);

            var ProductItem = await Context.ProductDetails
                .Include(Xd => Xd.Product)
                .SingleOrDefaultAsync(Bv => Bv.ProductDetailID == ShoppingCart.ProductDetailID);

            ShoppingCart.Price = ShoppingCart.QuantityCart * ProductItem?.Product?.Price;

            await Context.ShoppingCarts.AddAsync(ShoppingCart);
            await Context.SaveChangesAsync();

            return ShoppingCart;
        }

        public async Task Delete(Guid TargetID)
        {
            var Target = await GetShoppingCartByID(TargetID);
            if (Target != null)
            {
                Context.ShoppingCarts.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        // Sử dụng khi muốn làm sạch giỏ hàng hoặc/và **sau khi thanh toán xong & đã in hóa đơn**
        public async Task DeleteAllFromUserID(Guid TargetID)
        {
            var Targets = await GetShoppingCartsByUserID(TargetID);
            if (Targets != null)
            {
                Context.ShoppingCarts.RemoveRange(Targets);
                await Context.SaveChangesAsync();
            }
        }
        
        public async Task<ShoppingCart?> GetShoppingCartByID(Guid TargetID)
        {
            return await Context.ShoppingCarts
                //.Include(UU => UU.User)
                .Include(UU => UU.Voucher)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Category : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Color : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Size : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Sale : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                        .ThenInclude(WW => WW != null ? WW.Image : default)
                    .FirstOrDefaultAsync(x => x.CartID == TargetID);
        }

        public async Task<List<ShoppingCart>> GetShoppingCartsByUserID(Guid TargetID)
        {
            return await Context.ShoppingCarts
                //.Include(UU => UU.User)
                .Include(UU => UU.Voucher)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Category : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Color : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Size : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Sale : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                        .ThenInclude(WW => WW != null ? WW.Image : default)
                  .Where(UU => UU.UserID.Equals(TargetID)).ToListAsync();
        }

        public async Task<ShoppingCart?> GetShoppingCartByUserIDAndDetailID(Guid UserID, Guid TargetID)
        {
            return await Context.ShoppingCarts
                //.Include(UU => UU.User)
                .Include(UU => UU.Voucher)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Category : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Color : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Size : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Sale : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                        .ThenInclude(WW => WW != null ? WW.Image : default) // hot reload doesn't work
                .SingleOrDefaultAsync(WW => WW.UserID == UserID && WW.ProductDetailID == TargetID);
        }

        public async Task<List<ShoppingCart>> GetShoppingCarts()
        {
            return await Context.ShoppingCarts
                //.Include(UU => UU.User)
                .Include(UU => UU.Voucher)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Category : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Color : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Size : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Sale : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                        .ThenInclude(WW => WW != null ? WW.Image : default)
                    .ToListAsync();
        }

        public async Task<ShoppingCart?> Update(Guid TargetID, ShoppingCartDTO UpdatedShoppingCart)
        {
            var Target = await GetShoppingCartByID(TargetID);
            if (Target != null)
            {
                if (UpdatedShoppingCart.QuantityCart == 0)
                {
                    Context.Remove(Target);
                    await Context.SaveChangesAsync();
                    return default;
                }
                else
                {
                    Context.Entry(Target).State = EntityState.Modified;

                    Target = Mapper.Map(UpdatedShoppingCart, Target);
                    var ProductItem = Context.ProductDetails.Find(Target.ProductDetailID);
                    Target.Price = UpdatedShoppingCart.QuantityCart * ProductItem?.Product?.Price;

                    Context.Update(Target);
                    await Context.SaveChangesAsync();
                    return Target;
                }
            }
            else return default;
        }

        public async Task<ShoppingCart?> ApplyVoucher(Guid UserID, Guid ProductDetailID, string? VoucherCode)
        {
            var Target = await GetShoppingCartByUserIDAndDetailID(UserID, ProductDetailID);
            var FindVoucher = await Context.Vouchers.SingleOrDefaultAsync(Vc => Vc.VoucherCode == VoucherCode && Vc.Status != 0);

            if (Target != null && FindVoucher != null)
            {
                // Nếu như sản phẩm chưa có Voucher:
                // Gắn Voucher vào trong sản phẩm giỏ hàng đó
                // Sau đó trừ 1 lần dùng ở Voucher đó

                // Nếu như sản phẩm đã có Voucher:
                // Lấy dữ liệu Voucher cũ vả trả lại 1 lần dùng cho Voucher cũ đó
                // Sau đó tiến hành gắn Voucher mới vào trong sản phẩm giỏ hàng và trừ 1 lần dùng
                // Nếu gắn Voucher bị giống nhau thì nó sẽ chả làm gì cả

                if (Target.VoucherID != null)
                {
                    if (Target.VoucherID != FindVoucher.VoucherID)
                    {
                        Context.ShoppingCarts.Attach(Target);
                        Target.VoucherID = FindVoucher.VoucherID;
                        Context.Update(Target);

                        Context.Vouchers.Attach(FindVoucher);

                        if (FindVoucher.UsesLeft >= 1) FindVoucher.UsesLeft -= 1;
                        else return default;

                        Context.Update(FindVoucher);

                        var OldVoucher = await Context.Vouchers.FindAsync(Target.VoucherID);
                        if (OldVoucher != null)
                        {
                            Context.Vouchers.Attach(OldVoucher);
                            OldVoucher.UsesLeft += 1;
                            Context.Update(OldVoucher);
                        }

                        await Context.SaveChangesAsync();

                        return Target;
                    }
                    else return default;
                }
                else
                {
                    Context.ShoppingCarts.Attach(Target);
                    Target.VoucherID = FindVoucher.VoucherID;
                    Context.Update(Target);

                    Context.Vouchers.Attach(FindVoucher);
                    FindVoucher.UsesLeft -= 1;
                    Context.Update(FindVoucher);

                    await Context.SaveChangesAsync();

                    return Target;
                }
            }
            else return default;
        }

        public async Task<ShoppingCart?> UnapplyVoucher(Guid UserID, Guid ProductDetailID)
        {
            var Target = await GetShoppingCartByUserIDAndDetailID(UserID, ProductDetailID);
            if (Target != null)
            {
                var OldVoucher = await Context.Vouchers.FindAsync(Target.VoucherID);
                if (OldVoucher != null)
                {
                    Context.Vouchers.Attach(OldVoucher);
                    OldVoucher.UsesLeft += 1;
                    Context.Update(OldVoucher);
                }
                else return default;

                Context.ShoppingCarts.Attach(Target);
                Target.VoucherID = null;
                Context.Update(Target);

                await Context.SaveChangesAsync();

                return Target;
            }
            else return default;
        }

        public async Task<ShoppingCart?> Add2Cart(Guid UserID, Guid ProductDetailID, int? Quantity, bool? AdditionMode)
        {
            if (Quantity < 0) Quantity = 0;

            var CartItem = await GetShoppingCartByUserIDAndDetailID(UserID, ProductDetailID);

            if (CartItem != null)
            {
                Context.Entry(CartItem).State = EntityState.Modified;

                if (AdditionMode == true) CartItem.QuantityCart += (Quantity ?? 0);
                else CartItem.QuantityCart = (Quantity ?? 0);

                CartItem.Price = CartItem.QuantityCart * CartItem?.ProductDetail?.Product?.Price;

                if (CartItem!.QuantityCart == 0)
                {
                    Context.ShoppingCarts.Remove(CartItem);
                    await Context.SaveChangesAsync();
                    return default;
                }
                else
                {
                    Context.Update(CartItem!);
                    await Context.SaveChangesAsync();
                    return CartItem;
                }
            }
            else
            {
                ShoppingCartDTO NewCart = new()
                {
                    UserID = UserID,
                    ProductDetailID = ProductDetailID,
                    QuantityCart = Quantity ?? 1
                };

                var Response = await CreateNew(NewCart);
                await Context.SaveChangesAsync();

                return Response;
            }
        }
    }
}