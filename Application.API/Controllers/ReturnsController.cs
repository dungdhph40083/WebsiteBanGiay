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
    public class ReturnsController : ControllerBase
    {
        private readonly IReturn returnrepos;

        public ReturnsController(IReturn returnrepos)
        {
            this.returnrepos = returnrepos;
        }
        [HttpGet]
        public async Task<ActionResult<List<Return>>> Get()
        {
            return await returnrepos.GetReturn();
        }
        [HttpGet("{ID}")]
        public async Task<ActionResult<Return?>> Get(Guid ID)
        {
            return await returnrepos.GetReturnlByID(ID);
        }

        [HttpPost]
        public async Task<ActionResult<Return>> Post([FromBody] ReturnDTO NewReturn)
        {
            var Response = await returnrepos.CreateNew(NewReturn);
            return CreatedAtAction(nameof(Get), new { Response.ReturnID }, Response);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<Return?>> Put(Guid ID, [FromBody] ReturnDTO UpdateReturn)
        {
            return await returnrepos.UpdateExisting(ID, UpdateReturn);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await returnrepos.DeleteExisting(ID);
            return NoContent();
        }
    }
}
