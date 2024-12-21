using Microsoft.AspNetCore.Mvc;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class MyOrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
