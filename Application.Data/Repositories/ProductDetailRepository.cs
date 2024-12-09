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
            // để ko cần phải tự nhập GUID mỗi lần thêm dữ liệu nữa
            ProductDetail ProductDetail = new()
            {
                ProductDetailID = Guid.NewGuid(),
                UpdatedAt = DateTime.UtcNow
            };

            // có hai cách gán dữ liệu vào trong này:
            // cách 1: tự gán giá trị thuộc tính thủ công
            // VD: ProductDetail.WarrantyPeriod = DateTime.Now; bla bla bla
            // cách 2: dùng package gán dữ liệu từ một model con sang model mẹ (AutoMapper)
            // Source: nguồn >=====<CHẠY SANG>=====> Destination: đích đến
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
            // ?????????????????
            return await Context.ProductDetails
                    .Include(Prod => Prod.Product)
                    .Include(Img => Img.Image)
                    .Include(Siz => Siz.Size)
                    .Include(Col => Col.Color)
                    .Include(Ctg => Ctg.Category).SingleOrDefaultAsync(x => x.ProductDetailID == TargetID);
        }

        public Task<List<ProductDetail>> GetProductDetails()
        {
            // trích xuất cả dữ liệu image và product bằng cách .Include
            return Context.ProductDetails
                .Include(Prod => Prod.Product)
                .Include(Img => Img.Image)
                .Include(Siz => Siz.Size)
                .Include(Col => Col.Color)
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
