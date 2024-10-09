using Application.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface ICustomerSupportTicketsRepository
    {
        Task<IEnumerable<CustomerSupportTicketsDTO>> GetAllTickets();
        Task<CustomerSupportTicketsDTO> GetTicketById(Guid ticketId);
        Task CreateTicket(CustomerSupportTicketsDTO ticketDTO);
        Task UpdateTicket(CustomerSupportTicketsDTO ticketDTO);
        Task DeleteTicket(Guid ticketId);
    }
}
