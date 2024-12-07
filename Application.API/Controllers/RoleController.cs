using Application.API.Service;
using Application.Data.DTOs;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole Rolerepos;

        public RoleController(IRole Rolerepos)
        {
            this.Rolerepos = Rolerepos;
        }
        [HttpGet]
        public async Task<ActionResult<List<Role>>> get()
        {
            return await Rolerepos.GetRole();
        }
        [HttpGet("{ID}")]
        public async Task<ActionResult<Role?>> Get(Guid ID)
        {
            return await Rolerepos.GetRolelByID(ID);
        }

        [HttpPost]
        public async Task<ActionResult<Role>> Post([FromBody] RoleDTO NewRole)
        {
            return await Rolerepos.CreateNew(NewRole);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<Role?>> Put(Guid ID, [FromBody] RoleDTO UpdateRole)
        {
            return await Rolerepos.UpdateExisting(ID, UpdateRole);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await Rolerepos.DeleteExisting(ID);
            return Ok();
        }
    }
}
