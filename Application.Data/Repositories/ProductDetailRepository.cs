using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Repositories
{
    public class ProductDetailRepository : IProductDetail
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;
        public ProductDetailRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        public async Task<ProductDetail> CreateNew(ProductDetailDTO NewDetail)
        {
            ProductDetail ProductDetail = new()
            {
                ProductDetailID = Guid.NewGuid(),
                UpdatedAt = DateTime.UtcNow
            };

            ProductDetail = Mapper.Map(NewDetail, ProductDetail);
            await Context.ProductDetails.AddAsync(ProductDetail);
            await Context.SaveChangesAsync();
            return ProductDetail;
        }

        public async Task DeleteExisting(Guid TargetID)
        {
            var Target = await Context.ProductDetails.FindAsync(TargetID);
            if (Target != null)
            {
                Context.ProductDetails.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<ProductDetail?> GetProductDetailByID(Guid TargetID)
        {
            return await Context.ProductDetails
                    .Include(Prod => Prod.Product)
                    .Include(Siz => Siz.Size)
                    .Include(Col => Col.Color)
                    .Include(Img => Img.Image)
                    .Include(Ctg => Ctg.Category).SingleOrDefaultAsync(x => x.ProductDetailID == TargetID);
        }

        public async Task<ProductDetail?> GetProductDetailByProductID(Guid TargetID)
        {
            return await Context.ProductDetails
                    .Include(Prod => Prod.Product)
                    .Include(Siz => Siz.Size)
                    .Include(Col => Col.Color)
                    .Include(Img => Img.Image)
                    .Include(Ctg => Ctg.Category).SingleOrDefaultAsync(x => x.ProductID == TargetID);
        }

        public async Task<List<ProductDetail>> GetProductDetails()
        {
            // trích xuất cả dữ liệu image và product bằng cách .Include
            return await Context.ProductDetails
                .Include(Prod => Prod.Product)
                .Include(Siz => Siz.Size)
                .Include(Col => Col.Color)
                .Include(Img => Img.Image)
                .Include(Ctg => Ctg.Category).ToListAsync();
        }

        public async Task<ProductDetail?> UpdateExisting(Guid TargetID, ProductDetailDTO UpdatedDetail)
        {
            var Target = await Context.ProductDetails.FindAsync(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;

                Target.UpdatedAt = DateTime.UtcNow;

                // DO NOT REMOVE THIS LINE
                if (UpdatedDetail.ImageID == null) UpdatedDetail.ImageID = Target.ImageID;

                Target = Mapper.Map(UpdatedDetail, Target);
                Context.Update(Target);
                await Context.SaveChangesAsync();
                return Target;
            }
            else return default;
        }
    }
}
