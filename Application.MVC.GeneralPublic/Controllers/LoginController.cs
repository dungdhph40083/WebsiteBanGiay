using Microsoft.AspNetCore.Mvc;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
