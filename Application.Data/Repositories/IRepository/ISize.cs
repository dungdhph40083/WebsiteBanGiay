using Application.Data.DTOs;
using Application.Data.Models;

public interface ISize
{
    Task<List<Size>> GetSizes();
    Task<Size?> GetSizeByID(Guid TargetID);
    Task<Size> AddSize(SizeDTO NewSize);
    Task<Size?> UpdateSize(Guid TargetID, SizeDTO UpdatedSize);
    Task DeleteSize(Guid TargetID);
}