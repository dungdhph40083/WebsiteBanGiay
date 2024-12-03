using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Repositories
{
    public class User_RoleRepository : IUser_Role
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;
        public User_RoleRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        public async Task<User_Role> CreateNew(User_RoleDTO NewRelation)
        {
            User_Role User_Role = new() { User_RoleID = Guid.NewGuid() };
            User_Role = Mapper.Map(NewRelation, User_Role);
            await Context.User_Roles.AddAsync(User_Role);
            await Context.SaveChangesAsync();
            return User_Role;
        }

        public async Task DeleteRelation(Guid TargetID)
        {
            var Target = await GetUser_RoleByID(TargetID);
            if (Target != null)
            {
                Context.User_Roles.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<User_Role?> GetUser_RoleByID(Guid TargetID)
        {
            var Target = await Context.User_Roles
                .Include(UU => UU.User)
                .Include(UU => UU.Role)
                .SingleOrDefaultAsync(x => x.User_RoleID == TargetID);
            return Target;
        }

        public Task<List<User_Role>> GetUser_Roles()
        {
            return Context.User_Roles
                .Include(UU => UU.User)
                .Include(UU => UU.Role)
                .ToListAsync();
        }

        public async Task<User_Role?> UpdateExisting(Guid TargetID, User_RoleDTO UpdatedRelation)
        {
            var Target = await GetUser_RoleByID(TargetID);
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
