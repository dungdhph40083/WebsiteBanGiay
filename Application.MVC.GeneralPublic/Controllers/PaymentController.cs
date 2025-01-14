using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;
using System.Text;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class PaymentController : Controller
    {
        private readonly HttpClient _client;

        public PaymentController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        public async Task<IActionResult> PaymentCallbackVnpay
            (
                long vnp_Amount,
            string vnp_BankCode,
            string vnp_BankTranNo,
            string vnp_CardType,
            string vnp_OrderInfo,
            string vnp_PayDate,
            string vnp_ResponseCode,
            string vnp_TmnCode,
            string vnp_TransactionNo,
            string vnp_TransactionStatus,
            string vnp_TxnRef,
            string vnp_SecureHash
            )
        {
            try
            {
                if (vnp_TransactionStatus == "00")
                {
                    string URLUpdate = $@"https://localhost:7187/api/Orders/SetHasPaidToTrue/{vnp_TxnRef}";

                    var ResponseUpdate = await _client.PatchAsync(URLUpdate, null);

                    var ContentUpdate = JsonConvert.DeserializeObject<Order>
                        (await ResponseUpdate.Content.ReadAsStringAsync());

                    ViewBag.Message = "ĐÃ ĐẶT ĐƠN HÀNG VÀ THANH TOÁN THÀNH CÔNG.!";
                    ViewBag.MessageType = "success"; // Thành công
                }
                else if (vnp_TransactionStatus == "02")
                {
                    ViewBag.Message = "ĐÃ ĐẶT HÀNG NHƯNG CHƯA THANH TOÁN ĐƠN HÀNG!!!";
                    ViewBag.MessageType = "warning"; // Cảnh báo
                }
            }
            catch
            {
                ViewBag.Message = "Có lỗi xảy ra trong quá trình thanh toán";
                ViewBag.MessageType = "error"; // Lỗi
            }
            return View();
        }
    }
}
