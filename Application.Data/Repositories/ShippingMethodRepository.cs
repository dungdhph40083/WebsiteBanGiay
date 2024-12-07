using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class ShippingMethodRepository : IShippingMethod
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;
        public ShippingMethodRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        public async Task<ShippingMethod> CreateNew(ShippingMethodDTO NewShippingMethod)
        {
            ShippingMethod ShippingMethod = new() { ShippingMethodID = Guid.NewGuid() };
            ShippingMethod = Mapper.Map(NewShippingMethod, ShippingMethod);
            await Context.ShippingMethods.AddAsync(ShippingMethod);
            await Context.SaveChangesAsync();
            return ShippingMethod;
        }

        public async Task DeleteExisting(Guid TargetID)
        {
            var Target = await Context.ShippingMethods.FindAsync(TargetID);
            if (Target != null)
            {
                Context.ShippingMethods.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        public Task<List<ShippingMethod>> GetShippingMethod()
        {
            return Context.ShippingMethods.ToListAsync();
        }

        public async Task<ShippingMethod?> GetShippingMethodlByID(Guid TargetID)
        {
            return await Context.ShippingMethods
                    .SingleOrDefaultAsync(x => x.ShippingMethodID == TargetID);
        }

        public async Task<ShippingMethod?> UpdateExisting(Guid TargetID, ShippingMethodDTO UpdatedShippingMethod)
        {
            var Target = await Context.ShippingMethods.FindAsync(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                var UpdatedTarget = Mapper.Map(UpdatedShippingMethod, Target);
                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default!;
        }
    }
}
