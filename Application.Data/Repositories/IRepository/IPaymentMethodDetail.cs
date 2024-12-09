using Application.Data.DTOs;
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
        Task<List<PaymentMethodDetail>> GetAll();
        Task<PaymentMethodDetail?> GetById(Guid id);
        Task<PaymentMethodDetail> Add(PaymentMethodDetailDTO Details);
        Task<PaymentMethodDetail?> Update(Guid ID, PaymentMethodDetailDTO NewDetails);
        Task Delete(Guid id);
    }
}
