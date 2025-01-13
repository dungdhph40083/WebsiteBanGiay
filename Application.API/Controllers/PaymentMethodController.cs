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
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethod _paymentMethodRepository;

        public PaymentMethodController(IPaymentMethod paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaymentMethod>>> GetPaymentMethods()
        {
            return await _paymentMethodRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod?>> GetPaymentMethod(Guid id)
        {
            return await _paymentMethodRepository.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethodDTO paymentMethod)
        {
            var Response = await _paymentMethodRepository.Add(paymentMethod);
            return CreatedAtAction(nameof(GetPaymentMethod), new { id = Response.PaymentMethodID }, paymentMethod);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentMethod?>> PutPaymentMethod(Guid id, PaymentMethodDTO paymentMethod)
        {
            return await _paymentMethodRepository.Update(id, paymentMethod);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaymentMethod(Guid id)
        {
            await _paymentMethodRepository.Delete(id);
            return NoContent();
        }
    }
}
