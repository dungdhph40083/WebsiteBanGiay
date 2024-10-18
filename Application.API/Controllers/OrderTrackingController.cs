using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTrackingController : ControllerBase
    {
        private readonly IOrderTracking _orderTrackingRepository;

        public OrderTrackingController(IOrderTracking orderTrackingRepository)
        {
            _orderTrackingRepository = orderTrackingRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderTracking>> GetOrderTracking()
        {
            return Ok(_orderTrackingRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<OrderTracking> GetOrderTracking(Guid id)
        {
            var orderTracking = _orderTrackingRepository.GetById(id);

            if (orderTracking == null)
            {
                return NotFound();
            }

            return Ok(orderTracking);
        }

        [HttpPost]
        public ActionResult<OrderTracking> PostOrderTracking(OrderTracking orderTracking)
        {
            _orderTrackingRepository.Add(orderTracking);
            _orderTrackingRepository.Save();
            return CreatedAtAction("GetOrderTracking", new { id = orderTracking.TrackingID }, orderTracking);
        }

        [HttpPut("{id}")]
        public IActionResult PutOrderTracking(Guid id, OrderTracking orderTracking)
        {
            if (id != orderTracking.TrackingID)
            {
                return BadRequest();
            }

            _orderTrackingRepository.Update(orderTracking);
            _orderTrackingRepository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderTracking(int id)
        {
            _orderTrackingRepository.Delete(id);
            _orderTrackingRepository.Save();
            return NoContent();
        }
    }
}
