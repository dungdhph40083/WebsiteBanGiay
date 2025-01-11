using Application.API.Models;
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
    public class PaymentController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetails OrderDetailsRepository;
        private readonly IOrderTracking OrderTrackingRepo;
        private readonly IProductDetail ProductDetailRepo;
        private readonly IConfiguration _configuration;
        private readonly IShoppingCart ShoppingCartRepository;

        public PaymentController(IOrderRepository orderRepository, 
            IOrderTracking OrderTrackingRepo, IOrderDetails OrderDetailsRepository, 
            IProductDetail ProductDetailRepo, 
            IConfiguration configuration,
            IShoppingCart ShoppingCartRepository)
        {
            _orderRepository = orderRepository;
            this.OrderTrackingRepo = OrderTrackingRepo;
            this.OrderDetailsRepository = OrderDetailsRepository;
            this.ProductDetailRepo = ProductDetailRepo;
            this._configuration = configuration;
            this.ShoppingCartRepository = ShoppingCartRepository;
        }

        [HttpPost]
        public async Task<ActionResult<String?>> CreateOrder([FromBody] OrderDto NewOrder)
        {
            var CreatedOrder = await _orderRepository.CreateOrderAsync(NewOrder);
            var OrderDetailsCreatedResponse =
                await OrderDetailsRepository.ImportFromUserCart(NewOrder.UserID.GetValueOrDefault(), CreatedOrder.OrderID);

            var Thingies = await OrderDetailsRepository.GetOrderDetailsFromOrderID(CreatedOrder.OrderID);

            if (Thingies != null)
            {
                PaymentInformationModel model = new PaymentInformationModel();
                List<ShoppingCart> shoppingCarts = ShoppingCartRepository.GetShoppingCarts().Result;
                model.Amount = (double)shoppingCarts.Sum(x => x.Price) +
                   (double)(shoppingCarts.Count() > 0 ? ((shoppingCarts.Count() - 1) * 1000) : 0);
                model.OrderType = "other";
                model.OrderDescription = "Thanh toán qua VNPay hóa đơn " + CreatedOrder.OrderID;
                model.Name = model.HoTen;
                model.MaHoaDon = CreatedOrder.OrderID.ToString();

                var url = CreatePaymentUrl(model, HttpContext);

                await ShoppingCartRepository.DeleteAllFromUserID(NewOrder.UserID.GetValueOrDefault());
                return url;
            }
            else
            {
                return NotFound();
            }
        }

        private string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            //var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((long)model.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"{model.Name} {model.OrderDescription} {model.Amount}");
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", _configuration["Vnpay:returnUrl"]);
            pay.AddRequestData("vnp_TxnRef", model.MaHoaDon.ToString());

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }
    }
}
