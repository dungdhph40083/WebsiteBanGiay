using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    public class AAAAAAAAAAAAAAAAAAAAAController : ControllerBase
    {
        Random RNG = new Random();
        [HttpPost(nameof(GachaMachine))]
        public ActionResult GachaMachine(string Min, string Max)
        {
            long Minned, Maxxed;
            if (!long.TryParse(Max, out Maxxed))
            {
                return BadRequest("Invalid max!");
            }
            if (!long.TryParse(Min, out Minned) || Minned > Maxxed)
            {
                return BadRequest("Invalid min!");
            }
            return Ok(RNG.NextInt64(Minned, Maxxed));
        }
    }
}
