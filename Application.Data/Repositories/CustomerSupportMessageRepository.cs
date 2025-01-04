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

        public async Task<CustomerSupportMessage?> GetMessageByID(Guid Id)
        {
            return await Context.CustomerSupportMessages.FindAsync(Id);
        }

        public async Task<CustomerSupportMessage> SendMessage(CustomerSupportMessageDTO NewMessage)
        {
            // Tạo mới đối tượng CustomerSupportMessage
            CustomerSupportMessage customerSupportMessage = new()
            {
                MessageID = Guid.NewGuid(),  // Tạo MessageID mới
                CreatedAt = DateTime.Now, // Lưu thời gian tạo
                FirstName = NewMessage.FirstName, // Gán FirstName từ DTO
                Email = NewMessage.Email, // Gán Email từ DTO
                PhoneNumber = NewMessage.PhoneNumber, // Gán PhoneNumber từ DTO
                MessageContent = NewMessage.MessageContent, // Gán MessageContent từ DTO
                Status =1
            };

            // Thêm đối tượng vào cơ sở dữ liệu
            await Context.CustomerSupportMessages.AddAsync(customerSupportMessage);
            await Context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

            return customerSupportMessage; // Trả về đối tượng đã được lưu
        }
        public async Task<CustomerSupportMessage?> UpdateStatusOnly(Guid TargetID, byte Status)
        {
            var Target = await Context.CustomerSupportMessages.FindAsync(TargetID);
            if (Target != null)
            {
                // Đánh dấu mục này là đã được sửa đổi
                Context.CustomerSupportMessages.Attach(Target);

                // Chỉ cập nhật trường Status mà không thay đổi các trường khác
                Target.Status = Status;

                // Lưu thay đổi vào cơ sở dữ liệu
                await Context.SaveChangesAsync();

                return Target;
            }
            else return default; // Trả về null nếu không tìm thấy đối tượng
        }

    }
}
