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
            try
            {
                Guid? ID = GetCurrentUserId();
                if (ID != null)
                {
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
                    catch (UnauthorizedAccessException ex)
                    {
                        ViewBag.ErrorMessage = ex.Message;
                        return View(new User());
                    }
                }
                else return View();
            }
            catch (Exception Exc)
            {
                if (Exc.GetType() == typeof(UnauthorizedAccessException))
                {
                    return View();
                }
                else throw;
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
                catch (UnauthorizedAccessException ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View();
                }
            }
            return View(model);
        }
        private Guid GetCurrentUserId()
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Hãy Đăng Nhập Để Thực Hiện Chức Năng Này.");
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
