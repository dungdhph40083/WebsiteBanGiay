using Application.Data.DTOs;
using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface ISale
    {
        Task<List<Sale>> GetSale();
        Task<Sale?> GetSalelByID(Guid TargetID);
        Task<Sale> CreateNew(SaleDTO NewSale);
        Task<Sale?> UpdateExisting(Guid TargetID, SaleDTO UpdatedSale);
        Task DeleteExisting(Guid TargetID);
    }
}
