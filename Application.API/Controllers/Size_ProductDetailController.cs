using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Size_ProductDetailController : ControllerBase
    {
        private readonly ISize_ProductDetail Size_ProductDetailRepo;
        public Size_ProductDetailController(ISize_ProductDetail Size_ProductDetailRepo)
        {
            this.Size_ProductDetailRepo = Size_ProductDetailRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Size_ProductDetail>>> Get()
        {
            return await Size_ProductDetailRepo.GetSize_ProductDetails();
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<Size_ProductDetail?>> Get(Guid ID)
        {
            return await Size_ProductDetailRepo.GetSize_ProductDetailByID(ID);
        }

        [HttpPost]
        public async Task<ActionResult<Size_ProductDetail>> Post([FromBody] Size_ProductDetailDTO NewSize_ProductDetail)
        {
            return await Size_ProductDetailRepo.CreateNew(NewSize_ProductDetail);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<Size_ProductDetail?>> Put(Guid ID, [FromBody] Size_ProductDetailDTO UpdatedSize_ProductDetail)
        {
            return await Size_ProductDetailRepo.UpdateExisting(ID, UpdatedSize_ProductDetail);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await Size_ProductDetailRepo.DeleteRelation(ID);
            return Ok();
        }
    }
}
