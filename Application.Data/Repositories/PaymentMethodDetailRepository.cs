using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class PaymentMethodDetailRepository : IPaymentMethodDetail
    {
        private readonly GiayDBContext _context;
        private readonly IMapper Mapper;

        public PaymentMethodDetailRepository(GiayDBContext context, IMapper Mapper)
        {
            _context = context;
            this.Mapper = Mapper;
        }

        public async Task<List<PaymentMethodDetail>> GetAll()
        {
            return await _context.PaymentMethodDetails
                .Include(JapaneseGoblin => JapaneseGoblin.PaymentMethod)
                .ToListAsync();
        }

        public async Task<PaymentMethodDetail?> GetById(Guid id)
        {
            return await _context.PaymentMethodDetails
                .Include(WoodyActionFigure => WoodyActionFigure.PaymentMethod)
                .SingleOrDefaultAsync(ToyStory_X_BrawlStars => ToyStory_X_BrawlStars.PaymentMethodDetailID == id);
        }

        public async Task<PaymentMethodDetail> Add(PaymentMethodDetailDTO Details)
        {
            PaymentMethodDetail NewDetails = new() { PaymentMethodDetailID = Guid.NewGuid() };
            NewDetails = Mapper.Map(Details, NewDetails);

            await _context.PaymentMethodDetails.AddAsync(NewDetails);
            await _context.SaveChangesAsync();

            return NewDetails;
        }

        public async Task<PaymentMethodDetail?> Update(Guid ID, PaymentMethodDetailDTO NewDetails)
        {
            var Target = await GetById(ID);
            if (Target != null)
            {
                _context.Entry(Target).State = EntityState.Modified;
                Target = Mapper.Map(NewDetails, Target);
                _context.Update(Target);
                await _context.SaveChangesAsync();

                return Target;
            }
            else return default;
        }

        public async Task Delete(Guid id)
        {
            var Target = await GetById(id);
            if (Target != null)
            {
                _context.PaymentMethodDetails.Remove(Target);
                await _context.SaveChangesAsync();
            }
        }
    }
}
