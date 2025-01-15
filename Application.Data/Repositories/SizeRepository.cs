using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Aspose.Pdf;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace Application.Data.Repositories
{
    public class SizeRepository : ISize
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;
        public SizeRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }
        public async Task<Size> AddSize(SizeDTO NewSize)
        {
            var DateTimeUtcNow = DateTime.UtcNow;

            Size Size = new()
            {
                SizeID = Guid.NewGuid(),
                CreatedAt = DateTimeUtcNow,
                UpdatedAt = DateTimeUtcNow
            };
            Size = Mapper.Map(NewSize, Size);
            await Context.Sizes.AddAsync(Size);
            await Context.SaveChangesAsync();
            return Size;
        }

        public async Task DeleteSize(Guid TargetID)
        {
            var Target = await GetSizeByID(TargetID);
            if (Target != null)
            {
                Context.Sizes.Remove(Target);
                await Context.SaveChangesAsync(); // Sử dụng await cho SaveChangesAsync
            }
        }

        public Guid getProductDetail(Guid ProductId, Guid color, Guid size)
        {
            GiayDBContext Context = new GiayDBContext();
            if (Context.ProductDetails.Where(p => p.ProductID == ProductId
            && p.ColorID == color && p.SizeID == size).Distinct().ToList().Count == 0)
                return Guid.Empty;
            return Context.ProductDetails.Where(p => p.ProductID == ProductId
            && p.ColorID == color && p.SizeID == size).FirstOrDefault().ProductDetailID;
        }

        public Guid getProductDetail(Guid ProductId)
        {
            GiayDBContext Context = new GiayDBContext();
            if (Context.ProductDetails.Where(p => p.ProductID == ProductId
            && p.Quantity > 0).Distinct().ToList().Count == 0)
                return Guid.Empty;
            return Context.ProductDetails.Where(p => p.ProductID == ProductId
            && p.Quantity > 0).ToList()[0].ProductDetailID;
        }

        public int getQuantity(Guid ProductId, Guid color, Guid size)
        {
            GiayDBContext Context = new GiayDBContext();
            if (Context.ProductDetails.Where(p => p.ProductID == ProductId
            && p.ColorID == color && p.SizeID == size).Distinct().ToList().Count == 0)
                return 0;
            return Context.ProductDetails.Where(p => p.ProductID == ProductId
            && p.ColorID == color && p.SizeID == size).FirstOrDefault().Quantity.Value;
        }

        public int getQuantity(Guid ProductId)
        {
            GiayDBContext Context = new GiayDBContext();
            if (Context.ProductDetails.Where(p => p.ProductID == ProductId
            && p.Quantity > 0).Distinct().ToList().Count == 0)
                return 0;
            return Context.ProductDetails.Where(p => p.ProductID == ProductId
            && p.Quantity > 0).Distinct().ToList()[0].Quantity.Value;
        }

        public int getQuantityRemoveCart(Guid ProductId, Guid color, Guid size, Guid userId)
        {
            GiayDBContext Context = new GiayDBContext();
            if (Context.ProductDetails.Where(p => p.ProductID == ProductId
            && p.ColorID == color && p.SizeID == size).Distinct().ToList().Count == 0)
                return -1;
            int quantityCart = 0;
            if (Context.ShoppingCarts.Where(c => c.ProductDetail.ColorID == color
            && c.ProductDetail.SizeID == size && c.ProductDetail.ProductID == ProductId && c.UserID == userId)
                .FirstOrDefault() == null)
                quantityCart = 0;
            else
                quantityCart = Context.ShoppingCarts.Where(c => c.ProductDetail.ColorID == color
            && c.ProductDetail.SizeID == size && c.ProductDetail.ProductID == ProductId && c.UserID == userId)
                .FirstOrDefault().QuantityCart.Value;
            return Context.ProductDetails.Where(p => p.ProductID == ProductId
            && p.ColorID == color && p.SizeID == size).FirstOrDefault().Quantity.Value - quantityCart;
        }

        public async Task<Size?> GetSizeByID(Guid TargetID)
        {
            var Target = await Context.Sizes.FindAsync(TargetID);
            return Target;
        }

        public Task<List<Size>> GetSizes()
        {
            return Context.Sizes.ToListAsync();
        }

        public List<Size?> GetSizesByProductId(Guid ProductId)
        {
            GiayDBContext Context = new GiayDBContext();
            return Context.ProductDetails.Where(p => p.ProductID == ProductId).Select(p => p.Size)
            .Distinct().ToList();
        }

        public List<Size?> GetSizesByProductIdAndColor(Guid ProductId, Guid color)
        {
            GiayDBContext Context = new GiayDBContext();
            return Context.ProductDetails.Where(p => p.ProductID == ProductId && p.ColorID == color).Select(p => p.Size)
           .Distinct().ToList();
        }

        public async Task<bool> SizeNameAvailability(string? SizeName)
        {
            var Target = await Context.Sales.FirstOrDefaultAsync(LastClick => LastClick.Name == SizeName);
            if (Target == null) return true;
            else return false;
        }

        public async Task<Size?> UpdateSize(Guid TargetID, SizeDTO UpdatedSize)
        {
            var Target = await GetSizeByID(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                Target.UpdatedAt = DateTime.UtcNow;
                var UpdatedTarget = Mapper.Map(UpdatedSize, Target);
                UpdatedTarget.UpdatedAt = DateTime.UtcNow;
                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default;
        }
    }
}
