using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
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
        public ActionResult<IEnumerable<PaymentMethod>> GetPaymentMethods()
        {
            return Ok(_paymentMethodRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<PaymentMethod> GetPaymentMethod(Guid id)
        {
            var paymentMethod = _paymentMethodRepository.GetById(id);

            if (paymentMethod == null)
            {
                return NotFound();
            }

            return Ok(paymentMethod);
        }

        [HttpPost]
        public ActionResult<PaymentMethod> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            _paymentMethodRepository.Add(paymentMethod);
            _paymentMethodRepository.Save();
            return CreatedAtAction("GetPaymentMethod", new { id = paymentMethod.PaymentMethodID }, paymentMethod);
        }

        [HttpPut("{id}")]
        public IActionResult PutPaymentMethod(Guid id, PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.PaymentMethodID)
            {
                return BadRequest();
            }

            _paymentMethodRepository.Update(paymentMethod);
            _paymentMethodRepository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePaymentMethod(int id)
        {
            _paymentMethodRepository.Delete(id);
            _paymentMethodRepository.Save();
            return NoContent();
        }
    }
}
