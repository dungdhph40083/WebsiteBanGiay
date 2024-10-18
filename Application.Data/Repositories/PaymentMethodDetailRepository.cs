using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
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

        public PaymentMethodDetailRepository(GiayDBContext context)
        {
            _context = context;
        }

        public IEnumerable<PaymentMethodDetail> GetAll()
        {
            return _context.PaymentMethodDetails.ToList();
        }

        public PaymentMethodDetail GetById(Guid id)
        {
            return _context.PaymentMethodDetails.Find(id);
        }

        public void Add(PaymentMethodDetail paymentMethodDetail)
        {
            _context.PaymentMethodDetails.Add(paymentMethodDetail);
        }

        public void Update(PaymentMethodDetail paymentMethodDetail)
        {
            _context.PaymentMethodDetails.Update(paymentMethodDetail);
        }

        public void Delete(Guid id)
        {
            var paymentMethodDetail = _context.PaymentMethodDetails.Find(id);
            if (paymentMethodDetail != null)
            {
                _context.PaymentMethodDetails.Remove(paymentMethodDetail);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
