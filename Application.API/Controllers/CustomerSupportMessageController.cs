using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSupportMessageController : ControllerBase
    {
        private readonly ICustomerSupportMessage CustomerSupportMessageRepo;
        public CustomerSupportMessageController(ICustomerSupportMessage CustomerSupportMessageRepo)
        {
            this.CustomerSupportMessageRepo = CustomerSupportMessageRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerSupportMessage>>> Get()
        {
            return await CustomerSupportMessageRepo.GetAll();
        }

        [HttpGet]
        public async Task<ActionResult<CustomerSupportMessage?>> Get(long Time)
        {
            return await CustomerSupportMessageRepo.GetMessageByID(Time);
        }

        public async Task<ActionResult<CustomerSupportMessage>> Post(CustomerSupportMessageDTO NewMessage)
        {
            var Response = await CustomerSupportMessageRepo.SendMessage(NewMessage);
            return CreatedAtAction(nameof(Get), new {ID = Response.MessageID}, Response);
        }
    }
}
