using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface IUser_Role
    {
        Task<List<User_Role>> GetUser_Roles();
        Task<User_Role?> GetUser_RoleByID(Guid TargetID);
        Task<User_Role> CreateNew(User_RoleDTO NewRelation);
        Task<User_Role?> UpdateExisting(Guid TargetID, User_RoleDTO UpdatedRelation);
        Task DeleteRelation(Guid TargetID);
    }
}
