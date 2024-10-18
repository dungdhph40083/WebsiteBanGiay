using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Repositories
{
    public class UserRepository : IUser
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;
        public UserRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        public async Task<User> CreateUser(UserDTO NewUser)
        {
            User User = new() { UserID = Guid.NewGuid() };
            User = Mapper.Map(NewUser, User);
            await Context.Users.AddAsync(User);
            await Context.SaveChangesAsync();
            return User;
        }

        public async Task DeleteUser(Guid TargetID)
        {
            var Target = await GetUserByID(TargetID);
            if (Target != null)
            {
                Context.Users.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserByID(Guid TargetID)
        {
            var Target = await Context.Users.FindAsync(TargetID);
            return Target;
        }

        public Task<List<User>> GetUsers()
        {
            return Context.Users.ToListAsync();
        }

        public async Task<User?> UpdateUser(Guid TargetID, UserDTO UpdatedUser)
        {
            var Target = await GetUserByID(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                var UpdatedTarget = Mapper.Map(UpdatedUser, Target);
                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default;
        }
    }
}
