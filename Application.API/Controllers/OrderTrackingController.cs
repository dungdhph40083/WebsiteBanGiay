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

        [HttpGet("ByOrder/{ID}")]
        public async Task<ActionResult<List<OrderTracking>>> GetTrackingsByOrderID(Guid ID)
        {
            return await _orderTrackingRepository.GetStatusesByOrderID(ID);
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
    }
}
