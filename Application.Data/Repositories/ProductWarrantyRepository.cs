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
    public class ProductWarrantyRepository: IProductWarranty
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;

        public ProductWarrantyRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        public async Task<ProductWarranty> CreateNew(ProductWarrantyDTO NewWarranty)
        {
            var DateTimeUtcNow = DateTime.UtcNow;

            ProductWarranty ProductWarranty = new()
            {
                WarrantyID = Guid.NewGuid(),
                CreatedAt = DateTimeUtcNow,
                UpdatedAt = DateTimeUtcNow
            };
            ProductWarranty = Mapper.Map(NewWarranty, ProductWarranty);
            await Context.ProductWarranties.AddAsync(ProductWarranty);
            await Context.SaveChangesAsync();
            return ProductWarranty;
        }

        public async Task DeleteExisting(Guid TargetID)
        {
            var Target = await Context.ProductWarranties.FindAsync(TargetID);
            if (Target != null)
            {
                Context.ProductWarranties.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        public  Task<List<ProductWarranty>> GetProductWarranty()
        {
            return Context.ProductWarranties
               .Include(Prod => Prod.Product).ToListAsync();
        }

        public async Task<ProductWarranty?> GetProductWarrantylByID(Guid TargetID)
        {
            return await Context.ProductWarranties
                    .Include(Prod => Prod.Product).SingleOrDefaultAsync(x => x.WarrantyID == TargetID);
        }

        public async Task<ProductWarranty?> UpdateExisting(Guid TargetID, ProductWarrantyDTO UpdatedWarranty)
        {
            var Target = await Context.ProductWarranties.FindAsync(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                Target.UpdatedAt = DateTime.UtcNow;
                var UpdatedTarget = Mapper.Map(UpdatedWarranty, Target);
                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default;
        }
    }
}
