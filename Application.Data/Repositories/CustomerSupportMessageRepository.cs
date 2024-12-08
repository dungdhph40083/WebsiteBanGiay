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

        public async Task<List<CustomerSupportMessage>> GetAll()
        {
            return await Context.CustomerSupportMessages.ToListAsync();
        }

        public async Task<CustomerSupportMessage?> GetMessageByID(long Time)
        {
            return await Context.CustomerSupportMessages.FindAsync(Time);
        }

        public async Task<CustomerSupportMessage> SendMessage(CustomerSupportMessageDTO NewMessage)
        {
            CustomerSupportMessage CustomerSupportMessage = new() { MessageID = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() };
            CustomerSupportMessage = Mapper.Map(NewMessage, CustomerSupportMessage);
            await Context.CustomerSupportMessages.AddAsync(CustomerSupportMessage);
            await Context.SaveChangesAsync();

            return CustomerSupportMessage;
        }
    }
}
