using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderDetailsController : ControllerBase
	{
        private readonly IOrderDetails _orderDetailsRepository;

        public OrderDetailsController(IOrderDetails orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public ActionResult<IEnumerable<OrderDetail>> GetOrderDetails()
        {
            return Ok(_orderDetailsRepository.GetAll());
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public ActionResult<OrderDetail> GetOrderDetails(int id)
        {
            var orderDetails = _orderDetailsRepository.GetById(id);

            if (orderDetails == null)
            {
                return NotFound();
            }

            return Ok(orderDetails);
        }

        // POST: api/OrderDetails
        [HttpPost]
        public ActionResult<OrderDetail> PostOrderDetails(OrderDetail orderDetails)
        {
            _orderDetailsRepository.Add(orderDetails);
            _orderDetailsRepository.Save();
            return CreatedAtAction("GetOrderDetails", new { id = orderDetails.OrderDetailID }, orderDetails);
        }

        // PUT: api/OrderDetails/5
        [HttpPut("{id}")]
        public IActionResult PutOrderDetails(Guid id, OrderDetail orderDetails)
        {
            if (id != orderDetails.OrderDetailID)
            {
                return BadRequest();
            }

            _orderDetailsRepository.Update(orderDetails);
            _orderDetailsRepository.Save();

            return NoContent();
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderDetails(int id)
        {
            _orderDetailsRepository.Delete(id);
            _orderDetailsRepository.Save();
            return NoContent();
        }
    }
}
