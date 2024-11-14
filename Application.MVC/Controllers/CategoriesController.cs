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
    public class CategoriesController : Controller
    {
        HttpClient Client = new HttpClient();
        // GET: CategoriesController

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string URL = $@"https://localhost:7187/api/Category";
            var Response = await Client.GetFromJsonAsync<List<Category>>(URL);
            return View(Response);
        }

        // GET: CategoriesController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/Category/{ID}";
            var Response = await Client.GetFromJsonAsync<Category>(URL);
            return View(Response);
        }

        // GET: CategoriesController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryDTO Input)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Category";
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

        // GET: CategoriesController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/Category/{ID}";
            var Response = await Client.GetFromJsonAsync<CategoryDTO>(URL);

            return View(Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: CategoriesController/Edit/5
        public async Task<ActionResult> Edit(Guid ID, CategoryDTO NewInput)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Category/{ID}";
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

        // POST: CategoriesController/Delete/5
        public async Task<ActionResult> Delete(Guid ID)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Category/{ID}";
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
