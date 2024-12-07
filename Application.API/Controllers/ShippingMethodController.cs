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
    public class ShippingMethodController : ControllerBase
    {
        private readonly IShippingMethod ShippingMethodrepos;

        public ShippingMethodController(IShippingMethod ShippingMethodrepos)
        {
            this.ShippingMethodrepos = ShippingMethodrepos;
        }
        [HttpGet]
        public async Task<ActionResult<List<ShippingMethod>>> GetAll()
        {
            return await ShippingMethodrepos.GetShippingMethod();
        }
        [HttpGet("{ID}")]
        public async Task<ActionResult<ShippingMethod?>> Get(Guid ID)
        {
            return await ShippingMethodrepos.GetShippingMethodlByID(ID);
        }

        [HttpPost]
        public async Task<ActionResult<ShippingMethod>> Post([FromBody] ShippingMethodDTO NewShippingMethod)
        {
            return await ShippingMethodrepos.CreateNew(NewShippingMethod);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<ShippingMethod?>> Put(Guid ID, [FromBody] ShippingMethodDTO UpdateShippingMethod)
        {
            return await ShippingMethodrepos.UpdateExisting(ID, UpdateShippingMethod);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await ShippingMethodrepos.DeleteExisting(ID);
            return Ok();
        }
    }
}
