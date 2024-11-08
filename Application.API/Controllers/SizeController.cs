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

        [HttpGet("get-all")]
        public async Task<ActionResult<List<Size>>> Get()
        {
            return await SizeRepo.GetSizes();
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<Size?>> Get(Guid ID)
        {
            return await SizeRepo.GetSizeByID(ID);
        }

        [HttpPost("create-size")]
        public async Task<ActionResult<Size>> Post([FromBody] SizeDTO NewSize)
        {
            return await SizeRepo.AddSize(NewSize);
        }

        [HttpPut("update-size/{id}")]
        public async Task<ActionResult<Size?>> Put(Guid ID, [FromBody] SizeDTO UpdatedSize)
        {
            return await SizeRepo.UpdateSize(ID, UpdatedSize);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            var size = await SizeRepo.GetSizeByID(ID);
            if (size == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy
            }

            await SizeRepo.DeleteSize(ID);
            return NoContent();
        }
    }
}
