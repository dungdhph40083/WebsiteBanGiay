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
    public class ShippingMethodController : ControllerBase
    {
        private readonly IShippingMethod ShippingMethodrepos;

        public ShippingMethodController(IShippingMethod ShippingMethodrepos)
        {
            this.ShippingMethodrepos = ShippingMethodrepos;
        }
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<List<ShippingMethod>>> GetAll()
        {
            return await ShippingMethodrepos.GetShippingMethod();
        }
        [HttpGet("{ID}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<ShippingMethod?>> Get(Guid ID)
        {
            return await ShippingMethodrepos.GetShippingMethodlByID(ID);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<ShippingMethod>> Post([FromBody] ShippingMethodDTO NewShippingMethod)
        {
            var Response = await ShippingMethodrepos.CreateNew(NewShippingMethod);
            return CreatedAtAction(nameof(Get), new { Response.ShippingMethodID }, Response);
        }

        [HttpPut("{ID}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<ShippingMethod?>> Put(Guid ID, [FromBody] ShippingMethodDTO UpdateShippingMethod)
        {
            return await ShippingMethodrepos.UpdateExisting(ID, UpdateShippingMethod);
        }

        [HttpDelete("{ID}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await ShippingMethodrepos.DeleteExisting(ID);
            return NoContent();
        }
    }
}
