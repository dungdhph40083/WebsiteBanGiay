using Application.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IPaymentMethodDetail
    {
        IEnumerable<PaymentMethodDetail> GetAll();
        PaymentMethodDetail GetById(Guid id);
        void Add(PaymentMethodDetail paymentMethodDetail);
        void Update(PaymentMethodDetail paymentMethodDetail);
        void Delete(Guid id);
        void Save();
    }
}
