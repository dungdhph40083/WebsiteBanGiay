using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Repositories
{
    public class Size_ProductDetailRepository : ISize_ProductDetail
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;
        public Size_ProductDetailRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        public async Task<Size_ProductDetail> CreateNew(Size_ProductDetailDTO NewRelation)
        {
            Size_ProductDetail Relation = new() { Size_ProductDetail_ID = Guid.NewGuid() };
            Relation = Mapper.Map(NewRelation, Relation);
            await Context.Size_ProductDetails.AddAsync(Relation);
            await Context.SaveChangesAsync();
            return Relation;
        }

        public async Task DeleteRelation(Guid TargetID)
        {
            var Target = await GetSize_ProductDetailByID(TargetID);
            if (Target != null)
            {
                Context.Size_ProductDetails.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        public Task<List<Size_ProductDetail>> GetSize_ProductDetails()
        {
            return Context.Size_ProductDetails.ToListAsync();
        }

        public async Task<Size_ProductDetail?> GetSize_ProductDetailByID(Guid TargetID)
        {
            var Target = await Context.Size_ProductDetails.FindAsync(TargetID);
            return Target;
        }

        public async Task<Size_ProductDetail?> UpdateExisting(Guid TargetID, Size_ProductDetailDTO UpdatedRelation)
        {
            var Target = await GetSize_ProductDetailByID(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                var UpdatedTarget = Mapper.Map(UpdatedRelation, Target);
                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default;
        }
    }
}
