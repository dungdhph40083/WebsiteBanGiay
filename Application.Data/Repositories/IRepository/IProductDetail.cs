using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface IProductDetail
    {
        Task<List<ProductDetail>> GetProductDetails();
        Task<ProductDetail?> GetProductDetailByID(Guid TargetID);
        Task<List<ProductDetail>> GetProductDetailsByProductID(Guid TargetID);
        Task<List<ProductDetail>> GetVariationsByProductID(Guid? TargetID);
        Task<ProductDetail> CreateNew(ProductDetailDTO NewDetail);
        // Uses 2 fields only: ColorID & SizeID, in which it'll check whether if the variation type already exists, then don't create
        Task<ProductDetailVariationMetadata> CreateNewVariations(ProductDetailMultiDTO VariationDetails);
        Task<bool?> UpdateVariations(Guid ID, ProductDetailMultiDTO VariationDetails);
        Task<ProductDetail?> UpdateExisting(Guid TargetID, ProductDetailDTO UpdatedDetail);
        Task<ProductDetail?> UpdateSetToZero(Guid TargetID);
        Task<ProductDetail?> DoAddProductCount(Guid TargetID, int Count);
        Task<List<ProductDetail>> UpdateStatusOnly(Guid ProductID);
        Task DeleteExisting(Guid TargetID);
        Task DeleteExistingByProductID(Guid TargetID);
    }
}
