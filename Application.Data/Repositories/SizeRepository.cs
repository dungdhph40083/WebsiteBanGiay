using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
            Size Size = new() { SizeID = Guid.NewGuid() };
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

        public async Task<Size?> GetSizeByID(Guid TargetID)
        {
            var Target = await Context.Sizes.FindAsync(TargetID);
            return Target;
        }

        public Task<List<Size>> GetSizes()
        {
            return Context.Sizes.ToListAsync();
        }

        public async Task<Size?> UpdateSize(Guid TargetID, SizeDTO UpdatedSize)
        {
            var Target = await GetSizeByID(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
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
