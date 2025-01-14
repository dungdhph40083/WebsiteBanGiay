using Application.Data.DTOs;
using Application.Data.Enums;
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
            var DateTimeSync = DateTime.UtcNow;

            User User = new() { UserID = Guid.NewGuid(), CreatedAt = DateTimeSync, UpdatedAt = DateTimeSync};

            //NewUser.Password = PasswordHasher(NewUser.Password!);

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
                .Include(UU => UU.Image)
                .Include(RR => RR.Role).SingleOrDefaultAsync(x => x.UserID == TargetID);
            return Target;
        }

        public Task<List<User>> GetUsers()
        {
            return Context.Users
                .Include(UU => UU.Image)
                .Include(RR => RR.Role)
                .ToListAsync();
        }

        public async Task<User?> UpdateUser(Guid TargetID, UserDTO UpdatedUser)
        {
            var Target = await GetUserByID(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;

                // Cập nhật mật khẩu khi mật khẩu ko null
                if (!string.IsNullOrWhiteSpace(UpdatedUser.Password)) UpdatedUser.Password = /*PasswordHasher*/(UpdatedUser.Password);
                else UpdatedUser.Password = Target.Password;
                
                // Ko cập nhật ảnh khi ảnh null
                if (UpdatedUser.ImageID == null) UpdatedUser.ImageID = Target.ImageID;

                Target = Mapper.Map(UpdatedUser, Target);
                Target.UpdatedAt = DateTime.UtcNow;

                Context.Update(Target);
                await Context.SaveChangesAsync();
                return Target;
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

        public async Task<Guid?> ValidAccount(string Username, string Password)
        {
            var AccountFind = await Context.Users.SingleOrDefaultAsync(x => x.Username == Username);
            if (AccountFind != null && BallsCrypt.Verify(Password, AccountFind.Password))
            {
                return AccountFind.UserID;
            }
            else return default;
        }

        public async Task<bool> UsernameChecker(string Username)
        {
            var Target = await Context.Users.FirstOrDefaultAsync(Foda => Foda.Username == Username);
            if (Target == null) return true;
            else return false;
        }
    }
}
