using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface ICustomerSupportMessage
    {
        Task<List<CustomerSupportMessage>> GetAll();
        Task<CustomerSupportMessage?> GetMessageByID(long Time);
        Task<CustomerSupportMessage> SendMessage(CustomerSupportMessageDTO NewMessage);
    }
}
