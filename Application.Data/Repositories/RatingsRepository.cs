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
    public class RatingsRepository : IRatings
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;

        public RatingsRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }
        public async Task<Rating> CreateNew(RatingsDTO NewRating)
        {
            Rating Rating = new() { RatingID = Guid.NewGuid() };
            Rating = Mapper.Map(NewRating, Rating);
            await Context.Ratings.AddAsync(Rating);
            await Context.SaveChangesAsync();
            return Rating;
        }

        public async Task DeleteExisting(Guid TargetID)
        {
            var Target = await Context.Ratings.FindAsync(TargetID);
            if (Target != null)
            {
                Context.Ratings.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        public Task<List<Rating>> GetProductRating()
        {
            return Context.Ratings
               .Include(Prod => Prod.Product)
               .Include(user => user.User).ToListAsync();
        }

        public async Task<Rating?> GetProductRatinglByID(Guid TargetID)
        {
            return await Context.Ratings
                    .Include(Prod => Prod.Product)
                    .Include(user => user.User).SingleOrDefaultAsync(x => x.RatingID == TargetID);
        }

        public async Task<Rating?> UpdateExisting(Guid TargetID, RatingsDTO UpdatedRating)
        {
            var Target = await Context.Ratings.FindAsync(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                var UpdatedTarget = Mapper.Map(UpdatedRating, Target);
                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default!;
        }
    }
}
