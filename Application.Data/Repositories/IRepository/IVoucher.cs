using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface IVoucher
    {
        Task<List<Voucher>> GetVouchers();
        Task<Voucher?> GetVoucherByID(Guid TargetID);
        Task<Voucher> CreateVoucher(VoucherDTO NewVoucher);
        Task<Voucher?> UpdateVoucher(Guid TargetID, VoucherDTO UpdatedVoucher);
        Task DeleteVoucher(Guid TargetID);
        Task<string> VoucherValidator(Guid TargetID);
        Task<string> UseVoucher(Guid VoucherID, Guid CartID);
    }
}
