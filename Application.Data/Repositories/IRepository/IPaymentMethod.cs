using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IPaymentMethod
    {
        IEnumerable<PaymentMethod> GetAll();
        PaymentMethod GetById(Guid id);
        void Add(PaymentMethod paymentMethod);
        void Update(PaymentMethod paymentMethod);
        void Delete(int id);
        void Save();
    }
}
