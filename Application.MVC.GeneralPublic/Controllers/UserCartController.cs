using Microsoft.AspNetCore.Mvc;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class UserCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
