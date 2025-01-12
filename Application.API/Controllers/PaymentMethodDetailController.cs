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
    public class PaymentMethodDetailController : ControllerBase
    {
        private readonly IPaymentMethodDetail _paymentMethodDetailRepository;

        public PaymentMethodDetailController(IPaymentMethodDetail paymentMethodDetailRepository)
        {
            _paymentMethodDetailRepository = paymentMethodDetailRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaymentMethodDetail>>> GetPaymentMethodDetails()
        {
            return await _paymentMethodDetailRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethodDetail?>> GetPaymentMethodDetail(Guid id)
        {
            return await _paymentMethodDetailRepository.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentMethodDetail>> PostPaymentMethodDetail(PaymentMethodDetailDTO paymentMethodDetail)
        {
            var Response = await _paymentMethodDetailRepository.Add(paymentMethodDetail);
            return CreatedAtAction(nameof(GetPaymentMethodDetail), new { id = Response.PaymentMethodDetailID }, paymentMethodDetail);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentMethodDetail?>> PutPaymentMethodDetail(Guid id, PaymentMethodDetailDTO paymentMethodDetail)
        {
            return await _paymentMethodDetailRepository.Update(id, paymentMethodDetail);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaymentMethodDetail(Guid id)
        {
            await _paymentMethodDetailRepository.Delete(id);
            return NoContent();
        }
    }
}
