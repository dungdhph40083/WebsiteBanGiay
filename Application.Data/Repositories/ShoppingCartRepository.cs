using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

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

            var ProductItem = Context.Products.Find(ShoppingCart.ProductID);
            var VoucherItem = Context.Vouchers.Find(ShoppingCart.VoucherID);

            long    ItemPrice          = ProductItem == null ? 0 : ProductItem.Price;
            long    ItemVoucherPrice   = VoucherItem == null ? 0 : VoucherItem.DiscountPrice;
            decimal ItemVoucherPercent = VoucherItem == null ? 0 : VoucherItem.DiscountPercent;

            ShoppingCart.RawPrice =
                ShoppingCart.QuantityCart * ItemPrice;

            ShoppingCart.Discount =
                (long)(ItemVoucherPercent / 100 * ShoppingCart.RawPrice) + ItemVoucherPrice;

            ShoppingCart.FinalPrice =
                ShoppingCart.RawPrice - ShoppingCart.Discount;

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
        
        public async Task<ShoppingCart?> GetShoppingCartByID(Guid TargetID)
        {
            return await Context.ShoppingCarts
                .Include(UU => UU.User)
                .Include(UU => UU.Voucher)
                .Include(UU => UU.Size)
                .Include(UU => UU.Color)
                .Include(UU => UU.Product).FirstOrDefaultAsync(x => x.CartID == TargetID);
        }

        public async Task<List<ShoppingCart>> GetShoppingCarts()
        {
            return await Context.ShoppingCarts
                .Include(UU => UU.User)
                .Include(UU => UU.Voucher)
                .Include(UU => UU.Size)
                .Include(UU => UU.Color)
                .Include(UU => UU.Product).ToListAsync();
        }

        public async Task<ShoppingCart?> Update(Guid TargetID, ShoppingCartDTO UpdatedShoppingCart)
        {
            var Target = await GetShoppingCartByID(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                var UpdatedTarget = Mapper.Map(UpdatedShoppingCart, Target);

                var ProductItem = Context.Products.Find(Target.ProductID);
                var VoucherItem = Context.Vouchers.Find(Target.VoucherID);

                long ItemPrice = ProductItem == null ? 0 : ProductItem.Price;
                long ItemVoucherPrice = VoucherItem == null ? 0 : VoucherItem.DiscountPrice;
                decimal ItemVoucherPercent = VoucherItem == null ? 0 : VoucherItem.DiscountPercent;

                UpdatedTarget.RawPrice =
                    UpdatedShoppingCart.QuantityCart * ItemPrice;

                UpdatedTarget.Discount =
                    (long)(ItemVoucherPercent / 100 * UpdatedTarget.RawPrice) + ItemVoucherPrice;

                UpdatedTarget.FinalPrice =
                    UpdatedTarget.RawPrice - UpdatedTarget.Discount;

                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default;
        }
    }
}