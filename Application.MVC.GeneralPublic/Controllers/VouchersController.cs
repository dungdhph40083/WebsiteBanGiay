using Application.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Application.MVC.GeneralPublic.Controllers
{
    public class VouchersController : Controller
    {
        HttpClient Client = new();
        public async Task<ActionResult> Index()
        {
            string URL = $@"https://localhost:7187/api/Voucher";

            var Response = await Client.GetFromJsonAsync<List<Voucher>>(URL);

            if (Response != null)
            {
                var FilteredOutTheInactivesAndPrivateds =
                    Response.Where(Flt => Flt.Status == 1).ToList();

                var FilteredOutTheTimeConstraints =
                    FilteredOutTheInactivesAndPrivateds.Where
                    (Flt => DateTime.UtcNow >= Flt.StartingAt.GetValueOrDefault().AddDays(-1) &&
                            DateTime.UtcNow <= Flt.EndingAt.GetValueOrDefault()).ToList();

                return View(FilteredOutTheTimeConstraints);
            }
            else return View();
        }
    }
}
