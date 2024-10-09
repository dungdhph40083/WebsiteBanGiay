using Application.Data.DTOs;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSupportTicketsController : ControllerBase
    {
        private readonly ICustomerSupportTicketsRepository _customerSupportTicketsRepository;

        public CustomerSupportTicketsController(ICustomerSupportTicketsRepository customerSupportTicketsRepository)
        {
            _customerSupportTicketsRepository = customerSupportTicketsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _customerSupportTicketsRepository.GetAllTickets();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(Guid id)
        {
            var ticket = await _customerSupportTicketsRepository.GetTicketById(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CustomerSupportTicketDTO ticketDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Fake UserID logic if needed here
            ticketDTO.UserID = Guid.NewGuid();

            await _customerSupportTicketsRepository.CreateTicket(ticketDTO);
            return CreatedAtAction(nameof(GetTicketById), new { id = ticketDTO.TicketID }, ticketDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] CustomerSupportTicketDTO ticketDTO)
        {
            if (id != ticketDTO.TicketID)
            {
                return BadRequest();
            }

            await _customerSupportTicketsRepository.UpdateTicket(ticketDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            await _customerSupportTicketsRepository.DeleteTicket(id);
            return NoContent();
        }
    }
}
