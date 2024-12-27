using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.Repositories
{
    public class CustomerSupportMessageRepository : ICustomerSupportMessage
    {
        private readonly GiayDBContext Context;
        private readonly IMapper Mapper;

        public CustomerSupportMessageRepository(GiayDBContext Context, IMapper Mapper)
        {
            this.Context = Context;
            this.Mapper = Mapper;
        }

        public  Task<List<CustomerSupportMessage>> GetAll()
        {
            return Context.CustomerSupportMessages
                .Include(user => user.User).ToListAsync();
        }

        public async Task<CustomerSupportMessage?> GetMessageByID(Guid MsgID)
        {
            return await Context.CustomerSupportMessages.FindAsync(MsgID);
        }

        public async Task<CustomerSupportMessage> SendMessage(CustomerSupportMessageDTO NewMessage)
        {
            CustomerSupportMessage CustomerSupportMessage = new()
            {
                MessageID = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
            };
            CustomerSupportMessage = Mapper.Map(NewMessage, CustomerSupportMessage);
            await Context.CustomerSupportMessages.AddAsync(CustomerSupportMessage);
            await Context.SaveChangesAsync();
            return CustomerSupportMessage;
        }
    }
}
