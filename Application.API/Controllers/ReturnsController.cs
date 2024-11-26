using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Application.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Data.DTOs;

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
        [HttpGet("getbyId")]
        public async Task<ActionResult<Return?>> Get(Guid ID)
        {
            return await returnrepos.GetReturnlByID(ID);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Return>> Post([FromBody] ReturnDTO NewReturn)
        {
            return await returnrepos.CreateNew(NewReturn);
        }

        [HttpPut("update")]
        public async Task<ActionResult<Return?>> Put(Guid ID, [FromBody] ReturnDTO UpdateReturn)
        {
            return await returnrepos.UpdateExisting(ID, UpdateReturn);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await returnrepos.DeleteExisting(ID);
            return Ok();
        }
    }
}
