﻿using Application.Data.DTOs;
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
            Voucher Voucher = new() { VoucherID = Guid.NewGuid() };
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
            var Target = await Context.Vouchers.FindAsync(TargetID);
            return Target;
        }

        public Task<List<Voucher>> GetVouchers()
        {
            return Context.Vouchers.ToListAsync();
        }

        public async Task<Voucher?> UpdateVoucher(Guid TargetID, VoucherDTO UpdatedVoucher)
        {
            var Target = await GetVoucherByID(TargetID);
            if (Target != null)
            {
                Context.Entry(Target).State = EntityState.Modified;
                var UpdatedTarget = Mapper.Map(UpdatedVoucher, Target);
                Context.Update(UpdatedTarget);
                await Context.SaveChangesAsync();
                return UpdatedTarget;
            }
            else return default;
        }
    }
}
