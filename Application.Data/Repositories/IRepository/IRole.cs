using Application.Data.DTOs;
using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IRole
    {
        Task<List<Role>> GetRole();
        Task<Role?> GetRolelByID(Guid TargetID);
        Task<Role> CreateNew(RoleDTO NewRole);
        Task<Role?> UpdateExisting(Guid TargetID, RoleDTO UpdatedRole);
        Task DeleteExisting(Guid TargetID);
    }
}
