using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface IVoucher
    {
        Task<List<Voucher>> GetVouchers();
        Task<Voucher?> GetVoucherByID(Guid TargetID);
        Task<Voucher?> GetVoucherByUserID(Guid TargetID);
        Task<Voucher?> GetVoucherByVoucherCode(string VoucherCode);
        Task<Voucher> CreateVoucher(VoucherDTO NewVoucher);
        Task<Voucher?> UpdateVoucher(Guid TargetID, VoucherDTO UpdatedVoucher);
        Task<string> VoucherValidator(Guid UserID, string VoucherCode);
        Task<Voucher?> ToggleStatus(Guid ID);
        Task<Voucher?> StopVoucher(Guid ID);
        Task DeleteVoucher(Guid TargetID);
    }
}
