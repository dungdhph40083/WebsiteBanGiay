using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http.Headers;

namespace Application.MVC.Controllers
{
    public class SizeController : Controller
    {
        HttpClient Client = new HttpClient();
        private readonly HttpClient _httpClient;
        public SizeController()
        {
            _httpClient = new HttpClient();
        }
        // GET: SizeController

        [HttpGet]
        public async Task<ActionResult> Index()
        {

            string URL = $@"https://localhost:7187/api/Size";
            var Response = await Client.GetFromJsonAsync<List<Size>>(URL);
            return View(Response);
        }

        // GET: SizeController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(Guid ID)
        {

            string URL = $@"https://localhost:7187/api/Size/{ID}";
            var Response = await Client.GetFromJsonAsync<Size>(URL);
            return View(Response);
        }

        // GET: SizeController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SizeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SizeDTO Input)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Size";
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

        // GET: SizeController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid ID)
        {
            string URL = $@"https://localhost:7187/api/Size/{ID}";
            var Response = await Client.GetFromJsonAsync<SizeDTO>(URL);

            return View(Response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // POST: SizeController/Edit/5
        public async Task<ActionResult> Edit(Guid ID, SizeDTO NewInput)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Size/{ID}";
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

        // POST: SizeController/Delete/5
        public async Task<ActionResult> Delete(Guid ID)
        {
            try
            {
                string URL = $@"https://localhost:7187/api/Size/{ID}";
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
