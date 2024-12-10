using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface IProductDetail
    {
        Task<List<ProductDetail>> GetProductDetails();
        Task<ProductDetail?> GetProductDetailByID(Guid TargetID);
        Task<ProductDetail?> GetProductDetailByProductID(Guid TargetID);
        Task<ProductDetail> CreateNew(ProductDetailDTO NewDetail);
        Task<ProductDetail?> UpdateExisting(Guid TargetID, ProductDetailDTO UpdatedDetail);
        Task DeleteExisting(Guid TargetID);
    }
}
