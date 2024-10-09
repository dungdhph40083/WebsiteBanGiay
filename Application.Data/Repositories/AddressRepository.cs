using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly GiayDBContext _context;
        public AddressRepository(GiayDBContext context)
        {
            _context = context;
        }
        public async Task<AddressDTO> CreateAddress(AddressDTO addressDTO)
        {
            if (!await _context.Roles.AnyAsync(r => r.RoleID == addressDTO.RoleID))
            {
                Guid fakeRoleId = Guid.NewGuid();
                var fakeRole = new Role
                {
                    RoleID = fakeRoleId,
                    RoleName = "Fake Role" // Tên vai trò giả
                };
                _context.Roles.Add(fakeRole);
                await _context.SaveChangesAsync();
                addressDTO.RoleID = fakeRoleId; // Sử dụng RoleID đã giả
            }

            // Kiểm tra nếu UserID không tồn tại trong bảng Users
            if (!await _context.Users.AnyAsync(u => u.UserID == addressDTO.UserID))
            {
                // Fake UserID và tạo một User mới
                var fakeUserId = Guid.NewGuid();
                var newUser = new User
                {
                    UserID = fakeUserId,
                    Username = "Fake User", // Tên người dùng giả
                    Password = "FakePassword123" // Cần thiết lập Password
                };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                addressDTO.UserID = fakeUserId; // Cập nhật UserID mới
            }

            // Tạo mới địa chỉ với các thông tin đã cập nhật
            var address = new Address
            {
                AddressID = Guid.NewGuid(),
                UserID = addressDTO.UserID,
                RoleID = addressDTO.RoleID,
                Name = addressDTO.Name,
                PhoneNumber = addressDTO.PhoneNumber,
                Description = addressDTO.Description,
                DefaultAddress = addressDTO.DefaultAddress,
                Status = addressDTO.Status
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return addressDTO;


            //    var address = new Address
            //    {
            //        AddressID = addressDTO.AddressID,
            //        UserID = addressDTO.UserID,
            //        RoleID = addressDTO.RoleID,
            //        Name = addressDTO.Name,
            //        PhoneNumber = addressDTO.PhoneNumber,
            //        Description = addressDTO.Description,
            //        DefaultAddress = addressDTO.DefaultAddress,
            //        Status = addressDTO.Status,
            //    };
            //    _context.Addresses.Add(address);
            //    await _context.SaveChangesAsync();

            //    addressDTO.AddressID = address.AddressID;
            //    return addressDTO;
        }

        public async Task<bool> DeleteAddress(Guid id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return false;
            }
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Address> FindAsync(Guid id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task<AddressDTO> GetAddressById(Guid id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return null;
            }

            return new AddressDTO
            {
                AddressID = address.AddressID,
                UserID = address.UserID,
                RoleID = address.RoleID,
                Name = address.Name,
                PhoneNumber = address.PhoneNumber,
                Description = address.Description,
                DefaultAddress = address.DefaultAddress,
                Status = address.Status
            };
        }

        public async Task<IEnumerable<AddressDTO>> GetAllAddress()
        {
            return await _context.Addresses.Select(a => new AddressDTO
            {
                AddressID = a.AddressID,
                UserID = a.UserID,
                RoleID = a.RoleID,
                Name = a.Name,
                PhoneNumber = a.PhoneNumber,
                Description = a.Description,
                DefaultAddress = a.DefaultAddress,
                Status = a.Status
            }).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAddress(AddressDTO addressDTO)
        {
            var address = await _context.Addresses.FindAsync(addressDTO.AddressID);
            if (address == null)
            {
                return false;
            }

            address.UserID = addressDTO.UserID;
            address.RoleID = addressDTO.RoleID;
            address.Name = addressDTO.Name;
            address.PhoneNumber = addressDTO.PhoneNumber;
            address.Description = addressDTO.Description;
            address.DefaultAddress = addressDTO.DefaultAddress;
            address.Status = addressDTO.Status;

            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
