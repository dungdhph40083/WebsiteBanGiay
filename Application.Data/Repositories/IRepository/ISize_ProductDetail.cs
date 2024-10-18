using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface ISize_ProductDetail
    {
        Task<List<Size_ProductDetail>> GetSize_ProductDetails();
        Task<Size_ProductDetail?> GetSize_ProductDetailByID(Guid TargetID);
        Task<Size_ProductDetail> CreateNew(Size_ProductDetailDTO NewRelation);
        Task<Size_ProductDetail?> UpdateExisting(Guid TargetID, Size_ProductDetailDTO UpdatedRelation);
        Task DeleteRelation(Guid TargetID);
    }
}
