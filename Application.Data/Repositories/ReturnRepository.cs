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
    public class ReturnRepository : IReturn
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;
        public ReturnRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }
        public async Task<Return> CreateNew(ReturnDTO NewReturn)
        {
            Return Return = new() { ReturnID = Guid.NewGuid() };
            Return = Mapper.Map(NewReturn, Return);
            await Context.Returns.AddAsync(Return);
            await Context.SaveChangesAsync();
            return Return;
        }

        public async Task DeleteExisting(Guid TargetID)
        {
            var Target = await Context.Returns.FindAsync(TargetID);
            if (Target != null)
            {
                Context.Returns.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        public Task<List<Return>> GetReturn()
        {
            return Context.Returns
               .Include(order => order.Order).ToListAsync();
        }

        public async Task<Return?> GetReturnlByID(Guid TargetID)
        {
            return await Context.Returns
                    .Include(order => order.Order).SingleOrDefaultAsync(x => x.ReturnID == TargetID);
        }

        public async Task<Return?> UpdateExisting(Guid TargetID, ReturnDTO UpdatedReturn)
        {
            var Target = await Context.Returns.FindAsync(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                var UpdatedTarget = Mapper.Map(UpdatedReturn, Target);
                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default;
        }
    }
}
