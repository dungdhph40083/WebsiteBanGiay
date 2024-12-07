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
    public class RoleRepository :IRole
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;
        public RoleRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        public async Task<Role> CreateNew(RoleDTO NewRole)
        {
            Role Role = new() { RoleID = Guid.NewGuid() };
            Role = Mapper.Map(NewRole, Role);
            await Context.Roles.AddAsync(Role);
            await Context.SaveChangesAsync();
            return Role;
        }

        public async Task DeleteExisting(Guid TargetID)
        {
            var Target = await Context.Roles.FindAsync(TargetID);
            if (Target != null)
            {
                Context.Roles.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        public  Task<List<Role>> GetRole()
        {
            return Context.Roles.ToListAsync();
        }

        public async Task<Role?> GetRolelByID(Guid TargetID)
        {
            return await Context.Roles
                    .SingleOrDefaultAsync(x => x.RoleID == TargetID);
        }

        public async Task<Role?> UpdateExisting(Guid TargetID, RoleDTO UpdatedRole)
        {
            var Target = await Context.Roles.FindAsync(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                var UpdatedTarget = Mapper.Map(UpdatedRole, Target);
                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default;
        }
    }
}
