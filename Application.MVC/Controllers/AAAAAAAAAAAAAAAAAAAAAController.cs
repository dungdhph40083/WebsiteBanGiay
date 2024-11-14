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
    public class AAAAAAAAAAAAAAAAAAAAAController : Controller
    {
        HttpClient Client = new HttpClient();
        // GET: AAAAAAAAAAAAAAAAAAAAAController

        [HttpPost]
        public async Task<ActionResult> Index(long Min, long Max)
        {
            string URL = $@"https://localhost:7187/api/AAAAAAAAAAAAAAAAAAAAA?Min={Min.ToString()}&Max={Max.ToString()}";
            var Response = await Client.GetFromJsonAsync<List<AAAAAAAAAAAAAAAAAAAAA>>(URL);
            return View(Response);
        }
    }
}
