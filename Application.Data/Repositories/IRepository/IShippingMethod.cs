using Application.Data.DTOs;
using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IShippingMethod
    {
        Task<List<ShippingMethod>> GetShippingMethod();
        Task<ShippingMethod?> GetShippingMethodlByID(Guid TargetID);
        Task<ShippingMethod> CreateNew(ShippingMethodDTO NewShippingMethod);
        Task<ShippingMethod?> UpdateExisting(Guid TargetID, ShippingMethodDTO UpdatedShippingMethod);
        Task DeleteExisting(Guid TargetID);
    }
}
