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
    public class SaleController : ControllerBase
    {
        private readonly ISale Salerepos;

        public SaleController(ISale Salerepos)
        {
            this.Salerepos = Salerepos;
        }
        [HttpGet]
        public async Task<ActionResult<List<Sale>>> get()
        {
            return await Salerepos.GetSale();
        }
        [HttpGet("{ID}")]
        public async Task<ActionResult<Sale?>> Get(Guid ID)
        {
            return await Salerepos.GetSalelByID(ID);
        }

        [HttpPost]
        public async Task<ActionResult<Sale>> Post([FromBody] SaleDTO NewSale)
        {
            var Response = await Salerepos.CreateNew(NewSale);
            return CreatedAtAction(nameof(Get), new { Response.SaleID }, Response);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<Sale?>> Put(Guid ID, [FromBody] SaleDTO UpdateSale)
        {
            return await Salerepos.UpdateExisting(ID, UpdateSale);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await Salerepos.DeleteExisting(ID);
            return NoContent();
        }
    }
}
