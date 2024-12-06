using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using BallsCrypt = BCrypt.Net.BCrypt;

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
            DateTime DateTimeSync = DateTime.UtcNow;

            User User = new() { UserID = Guid.NewGuid(), CreatedAt = DateTimeSync, LastUpdatedOn = DateTimeSync};
            NewUser.Password = PasswordHasher(NewUser.Password);

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
            var Target = await Context.Users
                .Include(UU => UU.Image).SingleOrDefaultAsync(x => x.UserID == TargetID);
            return Target;
        }

        public Task<List<User>> GetUsers()
        {
            return Context.Users
                .Include(UU => UU.Image)
                .ToListAsync();
        }

        public async Task<User?> UpdateUser(Guid TargetID, UserDTO UpdatedUser)
        {
            var Target = await GetUserByID(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                UpdatedUser.Password = PasswordHasher(UpdatedUser.Password);
                var UpdatedTarget = Mapper.Map(UpdatedUser, Target);
                UpdatedTarget.LastUpdatedOn = DateTime.UtcNow;
                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default;
        }

        public async Task<bool> ToggleUser(Guid TargetID)
        {
            var Target = await GetUserByID(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                if (Target.Status == 1) Target.Status = 0;
                else Target.Status = 1;

                Context.Update(Target);
                await Context.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public string PasswordHasher(string Password)
        {
            return BallsCrypt.EnhancedHashPassword(Password, 14);
        }

        public async Task<bool> ValidAccount(string Username, string Password)
        {
            var AccountFind = await Context.Users.SingleOrDefaultAsync(x => x.Username == Username);
            if (AccountFind != null && BallsCrypt.Verify(Password, AccountFind.Password))
            {
                return true;
            }
            else return false;
        }
    }
}
