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
    public class PaymentMethodRepository :IPaymentMethod
    {
        private readonly GiayDBContext _context;

        public PaymentMethodRepository(GiayDBContext context)
        {
            _context = context;
        }

        public IEnumerable<PaymentMethod> GetAll()
        {
            return _context.PaymentMethods.ToList();
        }

        public PaymentMethod GetById(Guid id)
        {
            return _context.PaymentMethods.Find(id);
        }

        public void Add(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Add(paymentMethod);
        }

        public void Update(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Update(paymentMethod);
        }

        public void Delete(int id)
        {
            var paymentMethod = _context.PaymentMethods.Find(id);
            if (paymentMethod != null)
            {
                _context.PaymentMethods.Remove(paymentMethod);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
