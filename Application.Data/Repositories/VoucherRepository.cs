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

            Voucher.StartingAt = Voucher.StartingAt.GetValueOrDefault().ToUniversalTime();
            Voucher.EndingAt = Voucher.EndingAt.GetValueOrDefault().ToUniversalTime();

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

        public async Task<Voucher?> GetVoucherByUserID(Guid TargetID)
        {
            var Target = await Context.Users
                .SingleOrDefaultAsync(x => x.UserID == TargetID);
            if (Target != null)
            {
                var VoucherIs = await Context.Vouchers
                    .Include(UU => UU.Category)
                    .SingleOrDefaultAsync(x => x.VoucherID == Target.VoucherID);
                return VoucherIs;
            }
            else return default;
        }

        public Task<List<Voucher>> GetVouchers()
        {
            return Context.Vouchers
                .Include(UU => UU.Category)
                .OrderByDescending(VV => VV.CreatedAt)
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

                UpdatedTarget.StartingAt = UpdatedTarget.StartingAt.GetValueOrDefault().ToUniversalTime();
                UpdatedTarget.EndingAt = UpdatedTarget.EndingAt.GetValueOrDefault().ToUniversalTime();

                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default;
        }

        public async Task<string> VoucherValidator(Guid UserID, string VoucherCode)
        {
            var Target = await GetVoucherByVoucherCode(VoucherCode);
            if (Target != null)
            {
                #region Eye-destroyer
                var Shoppings = await Context.ShoppingCarts
                //.Include(UU => UU.User)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Category : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Color : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Size : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                .Include(UU => UU.ProductDetail)
                    .ThenInclude(VV => VV != null ? VV.Product : default)
                        .ThenInclude(WW => WW != null ? WW.Image : default)
                  .Where(UU => UU.UserID.Equals(UserID)).ToListAsync();
                #endregion

                if (Target.EndingAt < DateTime.UtcNow ||
                Target.Status == 0 ||
                Target.Status == 100) return ValidateErrorResult.VOUCHER_EXPIRED;

                if (DateTime.UtcNow < Target.StartingAt) return ValidateErrorResult.VOUCHER_IS_PREMATURE;

                if (Shoppings.Sum(P => P.Price) < Target.RequiredGrandTotal.GetValueOrDefault()) return ValidateErrorResult.VOUCHER_REQUIREMENT_FAIL;

                if (Target.UsesLeft == 0) return ValidateErrorResult.VOUCHER_RAN_OUT_OF_USES;

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
        public async Task<Voucher?> ToggleStatus(Guid ID)
        {
            var Target = await GetVoucherByID(ID);
            if (Target != null)
            {
                Context.Vouchers.Attach(Target);
                switch ((VoucherStatus)Target.Status.GetValueOrDefault())
                {
                    case VoucherStatus.Disabled:
                        Target.Status = (byte)VoucherStatus.Active;
                        break;
                    case VoucherStatus.Active:
                        Target.Status = (byte)VoucherStatus.Disabled;
                        break;
                    case VoucherStatus.DisabledPrivate:
                        Target.Status = (byte)VoucherStatus.ActivePrivate;
                        break;
                    case VoucherStatus.ActivePrivate:
                        Target.Status = (byte)VoucherStatus.DisabledPrivate;
                        break;
                    default:
                        break;
                }
                Context.Vouchers.Update(Target);
                await Context.SaveChangesAsync();
                return Target;
            }
            else return default;
        }
        public async Task<Voucher?> StopVoucher(Guid ID)
        {
            var Target = await GetVoucherByID(ID);
            if (Target != null && (Target.Status == 1 || Target.Status == 101))
            {
                Context.Vouchers.Attach(Target);
                Target.EndingAt = DateTime.UtcNow;
                switch ((VoucherStatus)Target.Status.GetValueOrDefault())
                {
                    case VoucherStatus.Active:
                        Target.Status = (byte)VoucherStatus.Disabled;
                        break;
                    case VoucherStatus.ActivePrivate:
                        Target.Status = (byte)VoucherStatus.DisabledPrivate;
                        break;
                    default:
                        break;
                }
                Context.Vouchers.Update(Target);
                await Context.SaveChangesAsync();
                return Target;
            }
            else return default;
        }
    }
}
