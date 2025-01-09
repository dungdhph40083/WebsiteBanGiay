using Application.Data.DTOs;
using Application.Data.Models;

namespace Application.Data.Repositories.IRepository
{
    public interface ICustomerSupportMessage
    {
        Task<List<CustomerSupportMessage>> GetAll();
        Task<CustomerSupportMessage?> GetMessageByID(Guid id);
        Task<CustomerSupportMessage> SendMessage(CustomerSupportMessageDTO NewMessage);
        Task<CustomerSupportMessage?> UpdateStatusOnly(Guid TargetID, byte Status);
    }
}
