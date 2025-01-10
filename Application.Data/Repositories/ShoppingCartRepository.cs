using Application.Data.DTOs;
using Application.Data.Enums;
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
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Category : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Color : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Size : default)
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
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Category : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Color : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Size : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                        .ThenInclude(WW => WW != null ? WW.Image : default)
                  .Where(UU => UU.UserID.Equals(TargetID)).ToListAsync();
        }

        public async Task DeleteShoppingCartsByDetailID(Guid TargetID)
        {
            var Target = await Context.ShoppingCarts
                //.Include(UU => UU.User)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Category : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Color : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Size : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                        .ThenInclude(WW => WW != null ? WW.Image : default)
                  .Where(UU => UU.ProductDetailID.Equals(TargetID)).ToListAsync();

            if (Target.Count > 0)
            {
                Context.ShoppingCarts.AttachRange(Target);
                Context.ShoppingCarts.RemoveRange(Target);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<ShoppingCart?> GetShoppingCartByUserIDAndDetailID(Guid UserID, Guid TargetID)
        {
            return await Context.ShoppingCarts
                //.Include(UU => UU.User)
                
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Category : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Color : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Size : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                        .ThenInclude(WW => WW != null ? WW.Image : default) // hot reload doesn't work
                .SingleOrDefaultAsync(WW => WW.UserID == UserID && WW.ProductDetailID == TargetID);
        }

        public async Task<List<ShoppingCart>> GetShoppingCarts()
        {
            return await Context.ShoppingCarts
                //.Include(UU => UU.User)
                
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Category : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Color : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Size : default)
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

        public async Task<string> ApplyVoucher(Guid UserID, string? VoucherCode)
        {
            var TargetUser = await Context.Users.SingleOrDefaultAsync(U => U.UserID == UserID);
            var TargetVoucher = await Context.Vouchers.SingleOrDefaultAsync(V => V.VoucherCode == VoucherCode);
            var Shoppings = await GetShoppingCartsByUserID(UserID);

            if (TargetUser == null) return ValidateErrorResult.BUT_NOBODY_CAME;

            if (string.IsNullOrWhiteSpace(VoucherCode))
            {
                var OldVoucher = await Context.Vouchers.SingleOrDefaultAsync(V => V.VoucherID == TargetUser.VoucherID);
                if (OldVoucher == null) return ValidateErrorResult.WTF_HOW_DID_IT_FAIL;

                if (OldVoucher.UsesLeft != -1)
                {
                    Context.Vouchers.Attach(OldVoucher);
                    OldVoucher.UsesLeft += 1;
                    Context.Update(OldVoucher);
                }

                Context.Users.Attach(TargetUser);
                TargetUser.VoucherID = null;
                Context.Update(TargetUser);
                await Context.SaveChangesAsync();

                return SuccessResult.VOUCHER_DISCARDED_SUCCESS;
            }
            else
            {
                if (TargetVoucher == null) return ValidateErrorResult.VOUCHER_DOES_NOT_EXIST;
                if (Shoppings.Sum(P => P.Price) < TargetVoucher.RequiredGrandTotal.GetValueOrDefault()) return ValidateErrorResult.VOUCHER_REQUIREMENT_FAIL;
                if (TargetVoucher.UsesLeft == 0) return ValidateErrorResult.VOUCHER_RAN_OUT_OF_USES;

                if (TargetUser.VoucherID != null)
                {
                    var OldVoucher = await Context.Vouchers.SingleOrDefaultAsync(V => V.VoucherID == TargetUser.VoucherID);
                    if (OldVoucher == null) return ValidateErrorResult.WTF_HOW_DID_IT_FAIL;

                    if (OldVoucher.UsesLeft != -1)
                    {
                        Context.Vouchers.Attach(OldVoucher);
                        OldVoucher.UsesLeft += 1;
                        Context.Update(OldVoucher);
                    }
                }

                Context.Users.Attach(TargetUser);
                TargetUser.VoucherID = TargetVoucher.VoucherID;

                Context.Update(TargetUser);

                if (TargetVoucher.UsesLeft != -1)
                {
                    Context.Vouchers.Attach(TargetVoucher);
                    TargetVoucher.UsesLeft -= 1;
                    Context.Update(TargetVoucher);
                }

                await Context.SaveChangesAsync();

                return SuccessResult.VOUCHER_APPLIANCE_SUCCESS;
            }
        }

        public async Task<ShoppingCart?> Add2Cart(Guid UserID, Guid ProductDetailID, int? Quantity, bool? AdditionMode)
        {
            if (Quantity < 0) Quantity = 0;

            var CartItem = await GetShoppingCartByUserIDAndDetailID(UserID, ProductDetailID);

            if (CartItem != null)
            {
                Context.Entry(CartItem).State = EntityState.Modified;

                if (AdditionMode == true)
                {
                    if (CartItem.ProductDetail?.Quantity.GetValueOrDefault() < (CartItem.QuantityCart + Quantity).GetValueOrDefault())
                    {
                        CartItem.QuantityCart = CartItem.ProductDetail.Quantity.GetValueOrDefault();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[MAXIMUM REACHED]\n" +
                            $"Product quant: {CartItem.ProductDetail?.Quantity.GetValueOrDefault()}\n" +
                            $"In cart: {CartItem.QuantityCart.GetValueOrDefault()}\n" +
                            $"Requested: {Quantity.GetValueOrDefault()}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        CartItem.QuantityCart += Quantity ?? 0;
                        Console.WriteLine($"[MAXIMUM NOT REACHED]\n" +
                            $"Product quant: {CartItem.ProductDetail?.Quantity.GetValueOrDefault()}\n" +
                            $"In cart: {CartItem.QuantityCart.GetValueOrDefault()}\n" +
                            $"Requested: {Quantity.GetValueOrDefault()}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else CartItem.QuantityCart = Quantity ?? 0;

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
                    QuantityCart = Quantity ?? 1,
                };

                var Response = await CreateNew(NewCart);
                await Context.SaveChangesAsync();

                return Response;
            }
        }
    }
}