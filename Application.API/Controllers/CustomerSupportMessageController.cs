using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerSupportMessageController : ControllerBase
    {
        private readonly ICustomerSupportMessage CustomerSupportMessageRepo;
        public CustomerSupportMessageController(ICustomerSupportMessage CustomerSupportMessageRepo)
        {
            this.CustomerSupportMessageRepo = CustomerSupportMessageRepo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<CustomerSupportMessage>>> Get()
        {
            return await CustomerSupportMessageRepo.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetById(Guid id)
        {
            var category = await CustomerSupportMessageRepo.GetMessageByID(id);
            if (category == null) return NotFound();

            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CustomerSupportMessage>> Post(CustomerSupportMessageDTO NewMessage)
        {
            var Response = await CustomerSupportMessageRepo.SendMessage(NewMessage);
            return CreatedAtAction(nameof(Get), new {ID = Response.MessageID}, Response);
        }
        [HttpPut("{ID}/toggle-status")]
        public async Task<ActionResult> ToggleStatus(Guid ID)
        {
            var customerSupportRepository = await CustomerSupportMessageRepo.GetMessageByID(ID);
            if (customerSupportRepository == null)
            {
                return NotFound("Product detail not found");
            }

            // Đảo trạng thái của productDetail
            byte newStatus = customerSupportRepository.Status == 1 ? (byte)0 : (byte)1;

            // Cập nhật trạng thái mà không thay đổi các thuộc tính khác
            await CustomerSupportMessageRepo.UpdateStatusOnly(ID, newStatus);

            // Trả về sản phẩm đã được cập nhật
            return Ok(await CustomerSupportMessageRepo.GetMessageByID(ID));
        }
    }
}
