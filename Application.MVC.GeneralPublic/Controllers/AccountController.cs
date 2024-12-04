using Microsoft.AspNetCore.Mvc;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
