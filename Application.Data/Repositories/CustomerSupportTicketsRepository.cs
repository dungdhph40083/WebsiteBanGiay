using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public class CustomerSupportTicketsRepository : ICustomerSupportTicketsRepository
    {
        private readonly GiayDBContext _context;

        public CustomerSupportTicketsRepository(GiayDBContext context)
        {
            _context = context;
        }
        public async Task CreateTicket(CustomerSupportTicketsDTO ticketDTO)
        {
            if (!await _context.Users.AnyAsync(u => u.UserID == ticketDTO.UserID))
            {
                // Fake UserID và tạo một User mới
                var fakeUserId = Guid.NewGuid();
                var newUser = new User
                {
                    UserID = fakeUserId,
                    Username = "Fake User", // Tên người dùng giả
                    Password = "FakePassword123" // Cần thiết lập Password
                };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                ticketDTO.UserID = fakeUserId; // Cập nhật UserID mới
            }


            // Tạo ticket hỗ trợ mới
            var ticket = new CustomerSupportTickets
            {
                TicketID = Guid.NewGuid(), // Tạo TicketID mới
                UserID = ticketDTO.UserID, // Sử dụng UserID đã xác định
                Subject = ticketDTO.Subject,
                Message = ticketDTO.Message,
                Status = ticketDTO.Status,
                CreateAt = DateTime.Now, // Ghi lại thời gian tạo
                CreateBy = ticketDTO.CreateBy // Gán người tạo (có thể là UserID hoặc tên người dùng)
            };

            // Thêm ticket vào cơ sở dữ liệu
            await _context.CustomerSupportTickets.AddAsync(ticket);
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }

        public async Task DeleteTicket(Guid ticketId)
        {
            var ticket = await _context.CustomerSupportTickets.FindAsync(ticketId);

            if (ticket != null)
            {
                _context.CustomerSupportTickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CustomerSupportTicketsDTO>> GetAllTickets()
        {
            return await _context.CustomerSupportTickets
           .Select(ticket => new CustomerSupportTicketsDTO
           {
               TicketID = ticket.TicketID,
               UserID = ticket.UserID,
               Subject = ticket.Subject,
               Message = ticket.Message,
               Status = ticket.Status,
               CreateAt = ticket.CreateAt,
               CreateBy = ticket.CreateBy
           }).ToListAsync();
        }

        public async Task<CustomerSupportTicketsDTO> GetTicketById(Guid ticketId)
        {
            var ticket = await _context.CustomerSupportTickets.FindAsync(ticketId);

            if (ticket == null)
            {
                return null;
            }

            return new CustomerSupportTicketsDTO
            {
                TicketID = ticket.TicketID,
                UserID = ticket.UserID,
                Subject = ticket.Subject,
                Message = ticket.Message,
                Status = ticket.Status,
                CreateAt = ticket.CreateAt,
                CreateBy = ticket.CreateBy
            };
        }

        public async Task UpdateTicket(CustomerSupportTicketsDTO ticketDTO)
        {
            var ticket = await _context.CustomerSupportTickets.FindAsync(ticketDTO.TicketID);

            if (ticket != null)
            {
                ticket.Subject = ticketDTO.Subject;
                ticket.Message = ticketDTO.Message;
                ticket.Status = ticketDTO.Status;
                ticket.CreateAt = ticketDTO.CreateAt;
                ticket.CreateBy = ticketDTO.CreateBy;

                _context.CustomerSupportTickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
        }
    }
}
