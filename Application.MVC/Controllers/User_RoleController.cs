using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace Application.MVC.Controllers
{
    public class User_RoleController : Controller
    {
        HttpClient Client = new HttpClient();
        // GET: User_RoleController

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string URL = $@"https://localhost:7187/api/User_Role";
            var Response = await Client.GetFromJsonAsync<List<User_Role>>(URL);
            return View(Response);
        }

        // GET: User_RoleController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/User_Role/{ID}";
            var Response = await Client.GetFromJsonAsync<User_Role>(URL);
            return View(Response);
        }

        // GET: User_RoleController/Create
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

            string URL_Users = $@"https://localhost:7187/api/User/";
            string URL_Roles = $@"https://localhost:7187/api/Role/";

            var UsersList = await Client.GetFromJsonAsync<List<User>>(URL_Users);
            var RolesList = await Client.GetFromJsonAsync<List<Role>>(URL_Roles);

            var UsersItems = UsersList?.Where(Wh => Wh.Status != 0)
                .Select(Cb => new SelectListItem
                   { Text = Cb.Username, Value = Cb.UserID.ToString() }).ToList();
            var RolesItems = RolesList?
                .Select(Cb => new SelectListItem
                   { Text = Cb.RoleName, Value = Cb.RoleID.ToString() }).ToList();

            ViewBag.Users = UsersItems;
            ViewBag.Roles = RolesItems;
        }

        // POST: User_RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User_RoleDTO Input)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/User_Role";
                var Response = await Client.PostAsJsonAsync(URL, Input);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }

        // GET: User_RoleController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid ID)
        {
            await FetchInfoPlsPlsPlsPls();
            string URL = $@"https://localhost:7187/api/User_Role/{ID}";
            var Response = await Client.GetFromJsonAsync<User_RoleDTO>(URL);

            return View(Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: User_RoleController/Edit/5
        public async Task<ActionResult> Edit(Guid ID, User_RoleDTO NewInput)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/User_Role/{ID}";
                var Response = await Client.PutAsJsonAsync(URL, NewInput);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }

        // POST: User_RoleController/Delete/5
        public async Task<ActionResult> Delete(Guid ID)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/User_Role/{ID}";
                var Response = await Client.DeleteAsync(URL);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Msg)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Msg.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return View();
            }
        }
    }
}
