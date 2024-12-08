using Application.Data.DTOs;
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
        public async Task<ActionResult<List<OrderTracking>>> GetOrderTracking()
        {
            return await _orderTrackingRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderTracking?>> GetOrderTracking(Guid id)
        {
            var orderTracking = await _orderTrackingRepository.GetById(id);

            if (orderTracking == null)
            {
                return NotFound();
            }

            return Ok(orderTracking);
        }

        [HttpPost]
        public async Task<ActionResult<OrderTracking>> PostOrderTracking(OrderTrackingDTO orderTracking)
        {
            await _orderTrackingRepository.Add(orderTracking);
            return CreatedAtAction(nameof(GetOrderTracking), new { id = orderTracking.TrackingID }, orderTracking);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderTracking?>> PutOrderTracking(Guid id, OrderTrackingDTO orderTracking)
        {
            var Response = await _orderTrackingRepository.Update(id, orderTracking);
            if (Response != null)
            {
                return Response;
            }
            else return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderTracking(Guid id)
        {
            await _orderTrackingRepository.Delete(id);
            return NoContent();
        }
    }
}
