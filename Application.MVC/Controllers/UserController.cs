using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net;

namespace Application.MVC.Controllers
{
    public class UserController : Controller
    {
        HttpClient Client = new HttpClient();
        // GET: UserController

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string URL = $@"https://localhost:7187/api/User";
            var Response = await Client.GetFromJsonAsync<List<User>>(URL);
            return View(Response);
        }

        // GET: UserController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/User/{ID}";
            var Response = await Client.GetFromJsonAsync<User>(URL);
            if (Response != null && Response.Image != null && Response.Image.ImageFileName != null)
            {
                ViewData["URLFetchImg"] = $@"https://localhost:7187/Images/{Response.Image.ImageFileName}";
            }
            return View(Response);
        }

        // GET: UserController/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await FetchInfoPlsPlsPlsPls();
            return View();
        }

        private async Task FetchInfoPlsPlsPlsPls()
        {
            // A pattern could have improved this but whatever
            // this project is already starting to get confusing

            string URL_Roles = $@"https://localhost:7187/api/Role/";

            var RolesList = await Client.GetFromJsonAsync<List<Role>>(URL_Roles);

            var RolesItems = RolesList?
                .Select(Cb => new SelectListItem
                { Text = Cb.RoleName, Value = Cb.RoleID.ToString() }).OrderBy(Ord => Ord.Value).ToList();

            ViewBag.Roles = RolesItems;
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserDTO Input, IFormFile? ProfilePic)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/User";

                MultipartFormDataContent Contents = new()
                {
                    { new StringContent(Input.Username!),                  nameof(Input.Username) },
                    { new StringContent(Input.Password ?? ""),             nameof(Input.Password) },
                    { new StringContent(Input.FirstName ?? ""),            nameof(Input.FirstName) },
                    { new StringContent(Input.LastName ?? ""),             nameof(Input.LastName) },
                    { new StringContent(Input.Email ?? ""),                nameof(Input.Email) },
                    { new StringContent(Input.Address ?? ""),              nameof(Input.Address) },
                    { new StringContent(Input.PhoneNumber ?? ""),          nameof(Input.PhoneNumber) },
                    { new StringContent(Input.Status.ToString() ?? "1"),   nameof(Input.Status) },
                };

                if (ProfilePic != null)
                {
                    var ImageStream = new StreamContent(ProfilePic.OpenReadStream());
                    Contents.Add(ImageStream, nameof(ProfilePic), ProfilePic.FileName);
                }

                var Response = await Client.PostAsync(URL, Contents);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View();
            }
        }

        // GET: UserController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid ID)
        {
            await FetchInfoPlsPlsPlsPls();
            string URL = $@"https://localhost:7187/api/User/{ID}";
            var Response = await Client.GetFromJsonAsync<UserDTO>(URL);

            return View(Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: UserController/Edit/5
        public async Task<ActionResult> Edit(Guid ID, UserDTO NewInput, IFormFile? NewProfilePic)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/User/{ID}";

                MultipartFormDataContent Contents = new()
                {
                    { new StringContent(NewInput.Username!),                nameof(NewInput.Username) },
                    { new StringContent(NewInput.Password ?? ""),           nameof(NewInput.Password) },
                    { new StringContent(NewInput.FirstName ?? ""),          nameof(NewInput.FirstName) },
                    { new StringContent(NewInput.LastName ?? ""),           nameof(NewInput.LastName) },
                    { new StringContent(NewInput.Email ?? ""),              nameof(NewInput.Email) },
                    { new StringContent(NewInput.Address ?? ""),            nameof(NewInput.Address) },
                    { new StringContent(NewInput.PhoneNumber ?? ""),        nameof(NewInput.PhoneNumber) },
                    { new StringContent(NewInput.Status.ToString() ?? "1"), nameof(NewInput.Status) },
                };

                if (NewProfilePic != null)
                {
                    var ImageStream = new StreamContent(NewProfilePic.OpenReadStream());
                    Contents.Add(ImageStream, nameof(NewProfilePic), NewProfilePic.FileName);
                }

                var Response = await Client.PutAsync(URL, Contents);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View();
            }
        }

        // PATCH: UserController/Toggle/5
        public async Task<ActionResult> Toggle(Guid ID)
        {
            try
            {
                var Content = new HttpRequestMessage()
                {
                    Method = HttpMethod.Patch,
                    RequestUri = new Uri($@"https://localhost:7187/api/User/Toggle/{ID}"),
                    Headers =
                    {
                        { HttpRequestHeader.Accept.ToString(), "*/*" }
                    }
                };
                var Response = await Client.SendAsync(Content);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View();
            }
        }

        // POST: UserController/Delete/5
        public async Task<ActionResult> Delete(Guid ID)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/User/{ID}";
                var Response = await Client.DeleteAsync(URL);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                TempData["Error"] = $"Đã có lỗi xảy ra! Lỗi:\n{Msg.Message} ({Msg.HResult})";
                return View();
            }
        }
    }
}
