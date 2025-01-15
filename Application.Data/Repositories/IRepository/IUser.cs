using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface IUser
    {
        Task<List<User>> GetUsers();
        Task<User?> GetUserByID(Guid TargetID);
        Task<User> CreateUser(UserDTO NewUser);
        Task<User?> UpdateUser(Guid TargetID, UserDTO UpdatedUser);
        Task<bool> ToggleUser(Guid TargetID);
        Task<Guid?> ValidAccount(string Username, string Password);
        Task<bool> UsernameChecker(string Username);

        // Task<bool> PhoneNumberChecker_4_VietnamLocale(string PhoneNumber);
        // Task<bool> Email(string Username);

        Task DeleteUser(Guid TargetID);
    }
}
