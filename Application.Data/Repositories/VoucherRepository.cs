using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Repositories
{
    public class VoucherRepository : IVoucher
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;
        public VoucherRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        public async Task<Voucher> CreateVoucher(VoucherDTO NewVoucher)
        {
            var DateTimeUtcNow = DateTime.UtcNow;

            Voucher Voucher = new()
            {
                VoucherID = Guid.NewGuid(),
                CreatedAt = DateTimeUtcNow,
                UpdatedAt = DateTimeUtcNow
            };

            NewVoucher.VoucherCode = NewVoucher.VoucherCode.ToUpper();
            Voucher = Mapper.Map(NewVoucher, Voucher);
            await Context.Vouchers.AddAsync(Voucher);
            await Context.SaveChangesAsync();
            return Voucher;
        }

        public async Task DeleteVoucher(Guid TargetID)
        {
            var Target = await GetVoucherByID(TargetID);
            if (Target != null)
            {
                Context.Vouchers.Remove(Target);
                await Context.SaveChangesAsync();
            }
        }

        public async Task<Voucher?> GetVoucherByID(Guid TargetID)
        {
            var Target = await Context.Vouchers
                .Include(UU => UU.Category)
                .SingleOrDefaultAsync(x => x.VoucherID == TargetID);
            return Target;
        }

        public Task<List<Voucher>> GetVouchers()
        {
            return Context.Vouchers
                .Include(UU => UU.Category)
                .ToListAsync();
        }

        public async Task<Voucher?> UpdateVoucher(Guid TargetID, VoucherDTO UpdatedVoucher)
        {
            var Target = await GetVoucherByID(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                UpdatedVoucher.VoucherCode = UpdatedVoucher.VoucherCode.ToUpper();
                var UpdatedTarget = Mapper.Map(UpdatedVoucher, Target);
                UpdatedTarget.UpdatedAt = DateTime.UtcNow;
                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default;
        }

        public async Task<string> VoucherValidator(string VoucherCode)
        {
            var Target = await GetVoucherByVoucherCode(VoucherCode);
            if (Target != null && Target.Status != 0)
            {
                if (DateTime.UtcNow > Target.EndingAt)
                {
                    return ValidateErrorResult.VOUCHER_EXPIRED;
                }
                if (DateTime.UtcNow < Target.StartingAt)
                {
                    return ValidateErrorResult.VOUCHER_IS_PREMATURE;
                }
                if (Target.UsesLeft <= 0)
                {
                    return ValidateErrorResult.VOUCHER_RAN_OUT_OF_USES;
                }
                return ValidateErrorResult.VOUCHER_VALID;
            }
            else return ValidateErrorResult.VOUCHER_DOES_NOT_EXIST;

            // public const string VOUCHER_EXPIRED = "VOUCHER_EXPIRED";
            // public const string VOUCHER_IS_PREMATURE = "VOUCHER_IS_PREMATURE";
            // public const string VOUCHER_RAN_OUT_OF_USES = "VOUCHER_RAN_OUT_OF_USES";
            // public const string VOUCHER_DOES_NOT_EXIST = "VOUCHER_DOES_NOT_EXIST";
            // public const string VOUCHER_VALID = "VOUCHER_VALID";
        }
        public async Task<Voucher?> GetVoucherByVoucherCode(string VoucherCode)
        {
            var Target = await Context.Vouchers
                .Include(UU => UU.Category)
                .SingleOrDefaultAsync(x => x.VoucherCode == VoucherCode);
            return Target;
        }
    }
}
