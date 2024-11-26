using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class SaleRepository : ISale
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;
        public SaleRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        public async Task<Sale> CreateNew(SaleDTO NewSale)
        {
            Sale sale = new() { SaleID = Guid.NewGuid() };
            sale = Mapper.Map(NewSale, sale);
            await Context.Sales.AddAsync(sale);
            await Context.SaveChangesAsync();
            return sale;
        }

        public async Task DeleteExisting(Guid TargetID)
        {
            var Target = await Context.Sales.FindAsync(TargetID);
            if (Target != null)
            {
                Context.Sales.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        public Task<List<Sale>> GetSale()
        {
            return Context.Sales
               .Include(order => order.Product).ToListAsync();
        }

        public async Task<Sale?> GetSalelByID(Guid TargetID)
        {
            return await Context.Sales
                    .Include(order => order.Product).SingleOrDefaultAsync(x => x.SaleID == TargetID);
        }

        public async Task<Sale?> UpdateExisting(Guid TargetID, SaleDTO UpdatedSale)
        {
            var Target = await Context.Sales.FindAsync(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                var UpdatedTarget = Mapper.Map(UpdatedSale, Target);
                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default!;
        }
    }
}
