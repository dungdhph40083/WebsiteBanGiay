using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface IUser
    {
        Task<List<User>> GetUsers();
        Task<User?> GetUserByID(Guid TargetID);
        Task<User> CreateUser(UserDTO NewUser);
        Task<User?> UpdateUser (Guid TargetID, UserDTO UpdatedUser);
        Task DeleteUser(Guid TargetID);
    }
}
