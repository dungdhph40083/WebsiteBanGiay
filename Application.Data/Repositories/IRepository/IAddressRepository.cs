using Application.Data.DTOs;
using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IAddressRepository
    {
        Task<IEnumerable<AddressDTO>> GetAllAddress();
        Task<AddressDTO> GetAddressById(Guid id);
        Task<AddressDTO> CreateAddress(AddressDTO addressDTO);
        Task<bool> UpdateAddress(AddressDTO addressDTO);
        Task<bool> DeleteAddress(Guid id);
        Task<Address> FindAsync(Guid id); 
        Task SaveChangesAsync();

    }
}
