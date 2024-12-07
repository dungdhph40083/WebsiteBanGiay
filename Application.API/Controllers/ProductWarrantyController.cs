using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWarrantyController : ControllerBase
    {
        private readonly IProductWarranty productWarrantyRepo;

        public ProductWarrantyController(IProductWarranty productWarrantyRepo)
        {
            this.productWarrantyRepo = productWarrantyRepo;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductWarranty>>> Get()
        {
            return await productWarrantyRepo.GetProductWarranty();
        }
        [HttpGet("{ID}")]
        public async Task<ActionResult<ProductWarranty?>> Get(Guid ID)
        {
            return await productWarrantyRepo.GetProductWarrantylByID(ID);
        }

        [HttpPost]
        public async Task<ActionResult<ProductWarranty>> Post([FromBody] ProductWarrantyDTO NewWarranty)
        {
            return await productWarrantyRepo.CreateNew(NewWarranty);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<ProductWarranty?>> Put(Guid ID, [FromBody] ProductWarrantyDTO UpdateWarranty)
        {
            return await productWarrantyRepo.UpdateExisting(ID, UpdateWarranty);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await productWarrantyRepo.DeleteExisting(ID);
            return Ok();
        }
    }
}
