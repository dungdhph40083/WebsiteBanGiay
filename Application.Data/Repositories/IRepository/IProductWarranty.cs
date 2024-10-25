using Application.Data.DTOs;
using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IProductWarranty
    {
        Task<List<ProductWarranty>> GetProductWarranty();
        Task<ProductWarranty?> GetProductWarrantylByID(Guid TargetID);
        Task<ProductWarranty> CreateNew(ProductWarrantyDTO NewWarranty);
        Task<ProductWarranty?> UpdateExisting(Guid TargetID, ProductWarrantyDTO UpdatedWarranty);
        Task DeleteExisting(Guid TargetID);
    }
}
