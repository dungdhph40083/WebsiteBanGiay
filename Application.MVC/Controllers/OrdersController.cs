using Application.Data.DTOs;
using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.MVC.Controllers
{
    public class OrdersController : Controller
    {
        HttpClient Client = new HttpClient();
        
        public async Task<ActionResult> Index()
        {
            string URL = "https://localhost:7187/Orders";

            var Response = await Client.GetFromJsonAsync<Order>(URL);
            return View(Response);
        }

        public async Task<ActionResult> Create(OrderDto NewOrder)
        {

        }
    }
}
