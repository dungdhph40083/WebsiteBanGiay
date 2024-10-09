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
        Task<IEnumerable<CustomerSupportTicketDTO>> GetAllTickets();
        Task<CustomerSupportTicketDTO> GetTicketById(Guid ticketId);
        Task CreateTicket(CustomerSupportTicketDTO ticketDTO);
        Task UpdateTicket(CustomerSupportTicketDTO ticketDTO);
        Task DeleteTicket(Guid ticketId);
    }
}
