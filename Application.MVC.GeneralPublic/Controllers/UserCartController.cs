using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class UserCartController : Controller
    {
        HttpClient Client = new HttpClient();

        public async Task<ActionResult> Index()
        {
            string URL = $@"HTTPS://LOCALHOST:7187/API/SHOPPINGCART";

            var Response = await Client.GetFromJsonAsync<List<ShoppingCart>>(URL);
            return View(Response);
        }
    }
}
