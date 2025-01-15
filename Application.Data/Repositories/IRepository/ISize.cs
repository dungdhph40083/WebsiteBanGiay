using Application.Data.DTOs;
using Application.Data.Models;

public interface ISize
{
    Task<List<Size>> GetSizes();
    List<Size?> GetSizesByProductId(Guid ProductId);
    List<Size?> GetSizesByProductIdAndColor(Guid ProductId, Guid color);
    int getQuantity(Guid ProductId, Guid color, Guid size);
    int getQuantity(Guid ProductId);
    int getQuantityRemoveCart(Guid ProductId, Guid color, Guid size, Guid userId);
    Guid getProductDetail(Guid ProductId, Guid color, Guid size);
    Guid getProductDetail(Guid ProductId);
    Task<Size?> GetSizeByID(Guid TargetID);
    Task<bool> SizeNameAvailability(string? SizeName);
    Task<Size> AddSize(SizeDTO NewSize);
    Task<Size?> UpdateSize(Guid TargetID, SizeDTO UpdatedSize);
    Task DeleteSize(Guid TargetID);
}