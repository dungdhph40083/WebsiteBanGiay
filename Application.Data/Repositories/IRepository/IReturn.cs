using Application.Data.DTOs;
using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IReturn
    {
        Task<List<Return>> GetReturn();
        Task<Return?> GetReturnlByID(Guid TargetID);
        Task<Return> CreateNew(ReturnDTO NewReturn);
        Task<Return?> UpdateExisting(Guid TargetID, ReturnDTO UpdatedReturn);
        Task DeleteExisting(Guid TargetID);
    }
}
