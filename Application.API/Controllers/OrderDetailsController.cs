using Application.Data.DTOs;
using Application.Data.Enums;
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
    [Authorize]
	public class OrderDetailsController : ControllerBase
	{
        private readonly IOrderDetails _orderDetailsRepository;
        private readonly IOrderRepository OrderRepository;
        private readonly IShoppingCart ShoppingCartRepository;

        public OrderDetailsController(IOrderDetails orderDetailsRepository, IOrderRepository OrderRepository, IShoppingCart ShoppingCartRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
            this.OrderRepository = OrderRepository;
            this.ShoppingCartRepository = ShoppingCartRepository;
        }

        // GET: api/OrderDetails
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IEnumerable<OrderDetail>> GetOrderDetails()
        {
            return await _orderDetailsRepository.GetAll();
        }

        [HttpGet("Order/{ID}")]
        public async Task<ActionResult<List<OrderDetail>>> GetOrderDetailsFromOrderID(Guid ID)
        {
            return await _orderDetailsRepository.GetOrderDetailsFromOrderID(ID);
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetails(Guid id)
        {
            var orderDetails =  await _orderDetailsRepository.GetById(id);

            if (orderDetails == null)
            {
                return NotFound();
            }

            return Ok(orderDetails);
        }

        // POST: api/OrderDetails
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<OrderDetail>> PostOrderDetails(OrderDetailDto orderDetails)
        {
            var Response = await _orderDetailsRepository.Add(orderDetails);
            return CreatedAtAction(nameof(GetOrderDetails), new { id = Response.OrderDetailID }, orderDetails);
        }

        [HttpPost("Checkout")]
        public async Task<ActionResult<List<OrderDetail>?>> CreateCheckoutOrder([FromBody] OrderDto NewOrder, string PaymentMethod)
        {
            if (PaymentMethod == PaymentMethods.CashOnDelivery && NewOrder.UserID != null)
            {
                var CreatedOrder = await OrderRepository.CreateOrderAsync(NewOrder);
                var OrderDetailsCreatedResponse =
                    await _orderDetailsRepository.ImportFromUserCart(NewOrder.UserID.GetValueOrDefault(), CreatedOrder.OrderID);

                var Thingies = await _orderDetailsRepository.GetOrderDetailsFromOrderID(CreatedOrder.OrderID);

                if (Thingies != null)
                {
                    await ShoppingCartRepository.DeleteAllFromUserID(NewOrder.UserID.GetValueOrDefault());
                    return Thingies;
                }
                else
                {
                    return NotFound();
                }
            }
            else return NotFound();
        }
    }
}