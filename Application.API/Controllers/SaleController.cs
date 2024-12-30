using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Application.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Data.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly ISale Salerepos;

        public SaleController(ISale Salerepos)
        {
            this.Salerepos = Salerepos;
        }
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<Sale>>> get()
        {
            return await Salerepos.GetSale();
        }
        [HttpGet("{ID}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<Sale?>> Get(Guid ID)
        {
            return await Salerepos.GetSalelByID(ID);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Sale>> Post([FromBody] SaleDTO NewSale)
        {
            var Response = await Salerepos.CreateNew(NewSale);
            return CreatedAtAction(nameof(Get), new { Response.SaleID }, Response);
        }

        [HttpPut("{ID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Sale?>> Put(Guid ID, [FromBody] SaleDTO UpdateSale)
        {
            return await Salerepos.UpdateExisting(ID, UpdateSale);
        }

        [HttpDelete("{ID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await Salerepos.DeleteExisting(ID);
            return NoContent();
        }
    }
}
