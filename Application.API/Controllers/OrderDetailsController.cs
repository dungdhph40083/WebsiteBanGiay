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
        public async Task<ActionResult<OrderDetail>> PostOrderDetails(OrderDetailDto orderDetails)
        {
            var Response = await _orderDetailsRepository.Add(orderDetails);
            return CreatedAtAction(nameof(GetOrderDetails), new { id = Response.OrderDetailID }, orderDetails);
        }
    }
}
