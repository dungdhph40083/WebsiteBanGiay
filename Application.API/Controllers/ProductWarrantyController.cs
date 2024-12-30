using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductWarrantyController : ControllerBase
    {
        private readonly IProductWarranty productWarrantyRepo;

        public ProductWarrantyController(IProductWarranty productWarrantyRepo)
        {
            this.productWarrantyRepo = productWarrantyRepo;
        }
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<ProductWarranty>>> Get()
        {
            return await productWarrantyRepo.GetProductWarranty();
        }
        [HttpGet("{ID}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<ProductWarranty?>> Get(Guid ID)
        {
            return await productWarrantyRepo.GetProductWarrantylByID(ID);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductWarranty>> Post([FromBody] ProductWarrantyDTO NewWarranty)
        {
            var Response = await productWarrantyRepo.CreateNew(NewWarranty);
            return CreatedAtAction(nameof(Get), new { Response.WarrantyID }, Response);
        }

        [HttpPut("{ID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductWarranty?>> Put(Guid ID, [FromBody] ProductWarrantyDTO UpdateWarranty)
        {
            return await productWarrantyRepo.UpdateExisting(ID, UpdateWarranty);
        }

        [HttpDelete("{ID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await productWarrantyRepo.DeleteExisting(ID);
            return NoContent();
        }
    }
}
