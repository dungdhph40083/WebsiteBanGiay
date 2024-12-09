using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class PaymentMethodRepository : IPaymentMethod
    {
        private readonly GiayDBContext _context;
        private readonly IMapper Mapper;

        public PaymentMethodRepository(GiayDBContext context, IMapper Mapper)
        {
            _context = context;
            this.Mapper = Mapper;
        }

        public async Task<List<PaymentMethod>> GetAll()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod?> GetById(Guid id)
        {
            return await _context.PaymentMethods.FindAsync(id);
        }

        public async Task<PaymentMethod> Add(PaymentMethodDTO paymentMethod)
        {
            var DateTimeUtcNow = DateTime.UtcNow;
            PaymentMethod NewMethod = new()
            {
                PaymentMethodID = Guid.NewGuid(),
                CreatedAt = DateTimeUtcNow,
                UpdatedAt = DateTimeUtcNow
            };

            NewMethod = Mapper.Map(paymentMethod, NewMethod);

            await _context.PaymentMethods.AddAsync(NewMethod);
            await _context.SaveChangesAsync();

            return NewMethod;
        }

        public async Task<PaymentMethod?> Update(Guid ID, PaymentMethodDTO paymentMethod)
        {
            var Target = await GetById(ID);
            if (Target != null)
            {
                _context.Entry(Target).State = EntityState.Modified;
                Target.UpdatedAt = DateTime.UtcNow;

                Target = Mapper.Map(paymentMethod, Target);
                _context.Update(Target);
                await _context.SaveChangesAsync();

                return Target;
            }
            else return default;
        }

        public async Task Delete(Guid ID)
        {
            var Target = await GetById(ID);
            if (Target != null)
            {
                _context.PaymentMethods.Remove(Target);
                await _context.SaveChangesAsync();
            }
        }
    }
}
