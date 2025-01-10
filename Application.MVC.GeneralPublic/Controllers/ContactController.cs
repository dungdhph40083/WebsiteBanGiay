using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class ContactController : Controller
    {
        private readonly ICustomerSupportMessage _customerSupportRepository;
        private readonly HttpClient client = new HttpClient();

        public ContactController(ICustomerSupportMessage customerSupportRepository)
        {
            _customerSupportRepository = customerSupportRepository ?? throw new ArgumentNullException(nameof(customerSupportRepository));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
          

            Guid ID = GetCurrentUserId();
            try
            {
                var user = await client.GetFromJsonAsync<User>($@"https://localhost:7187/api/User/{ID}");
                ViewBag.DefaultUser = user;

                if (TempData["SuccessMessage"] != null)
                {
                    ViewBag.SuccessMessage = TempData["SuccessMessage"];
                }

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerSupportMessage model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var messageDto = new CustomerSupportMessageDTO
                    {
                        FirstName = model.FirstName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        MessageContent = model.MessageContent,
                        CreatedAt = DateTime.UtcNow,
                        Status = 1
                    };

                    var result = await _customerSupportRepository.SendMessage(messageDto);

                    if (result != null)
                    {
                        TempData["SuccessMessage"] = "Tin nhắn đã được gửi thành công!";
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Có lỗi khi gửi tin nhắn!";
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
                }
            }
            return View(model);
        }
        private Guid GetCurrentUserId()
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token không tồn tại. Vui lòng đăng nhập lại.");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("UserId không tồn tại trong token.");
            }

            return Guid.Parse(userIdClaim.Value);
        }

    }

}
