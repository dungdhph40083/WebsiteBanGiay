using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISize SizeRepo;
        public SizeController(ISize SizeRepo)
        {
            this.SizeRepo = SizeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Size>>> Get()
        {
            return await SizeRepo.GetSizes();
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<Size?>> Get(Guid ID)
        {
            var Response = await SizeRepo.GetSizeByID(ID);
            if (Response == null) return NoContent();
            else return Ok(Response);
        }

        [HttpPost]
        public async Task<ActionResult<Size>> Post([FromBody] SizeDTO NewSize)
        {
            var Response = await SizeRepo.AddSize(NewSize);
            return CreatedAtAction(nameof(Get), new { ID = Response.SizeID }, Response);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<Size?>> Put(Guid ID, [FromBody] SizeDTO UpdatedSize)
        {
            var Response = await SizeRepo.UpdateSize(ID, UpdatedSize);
            if (Response == null) return NotFound();
            else return Response;
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await SizeRepo.DeleteSize(ID);
            return NoContent();
        }
    }
}
