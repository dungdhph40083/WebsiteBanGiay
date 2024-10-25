using Application.Data.DTOs;
using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IRatings
    {
        Task<List<Rating>> GetProductRating();
        Task<Rating?> GetProductRatinglByID(Guid TargetID);
        Task<Rating> CreateNew(RatingsDTO NewRating);
        Task<Rating?> UpdateExisting(Guid TargetID, RatingsDTO UpdatedRating);
        Task DeleteExisting(Guid TargetID);
    }
}
