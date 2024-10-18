using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
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
        public ActionResult<IEnumerable<PaymentMethodDetail>> GetPaymentMethodDetails()
        {
            return Ok(_paymentMethodDetailRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<PaymentMethodDetail> GetPaymentMethodDetail(Guid id)
        {
            var paymentMethodDetail = _paymentMethodDetailRepository.GetById(id);

            if (paymentMethodDetail == null)
            {
                return NotFound();
            }

            return Ok(paymentMethodDetail);
        }

        [HttpPost]
        public ActionResult<PaymentMethodDetail> PostPaymentMethodDetail(PaymentMethodDetail paymentMethodDetail)
        {
            _paymentMethodDetailRepository.Add(paymentMethodDetail);
            _paymentMethodDetailRepository.Save();
            return CreatedAtAction("GetPaymentMethodDetail", new { id = paymentMethodDetail.PaymentMethodDetailID }, paymentMethodDetail);
        }

        [HttpPut("{id}")]
        public IActionResult PutPaymentMethodDetail(Guid id, PaymentMethodDetail paymentMethodDetail)
        {
            if (id != paymentMethodDetail.PaymentMethodDetailID)
            {
                return BadRequest();
            }

            _paymentMethodDetailRepository.Update(paymentMethodDetail);
            _paymentMethodDetailRepository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePaymentMethodDetail(Guid id)
        {
            _paymentMethodDetailRepository.Delete(id);
            _paymentMethodDetailRepository.Save();
            return NoContent();
        }
    }
}
