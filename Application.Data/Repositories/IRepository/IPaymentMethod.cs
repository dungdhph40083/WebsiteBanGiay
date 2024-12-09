using Application.Data.DTOs;
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
        Task<List<PaymentMethod>> GetAll();
        Task<PaymentMethod?> GetById(Guid id);
        Task<PaymentMethod> Add(PaymentMethodDTO paymentMethod);
        Task<PaymentMethod?> Update(Guid ID, PaymentMethodDTO paymentMethod);
        Task Delete(Guid ID);
    }
}
