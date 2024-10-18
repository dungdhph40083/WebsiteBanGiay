using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_RoleController : ControllerBase
    {
        private readonly IUser_Role User_RoleRepo;
        public User_RoleController(IUser_Role User_RoleRepo)
        {
            this.User_RoleRepo = User_RoleRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<User_Role>>> Get()
        {
            return await User_RoleRepo.GetUser_Roles();
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<User_Role?>> Get(Guid ID)
        {
            return await User_RoleRepo.GetUser_RoleByID(ID);
        }

        [HttpPost]
        public async Task<ActionResult<User_Role>> Post([FromBody] User_RoleDTO NewUser_Role)
        {
            return await User_RoleRepo.CreateNew(NewUser_Role);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<User_Role?>> Put(Guid ID, [FromBody] User_RoleDTO UpdatedUser_Role)
        {
            return await User_RoleRepo.UpdateExisting(ID, UpdatedUser_Role);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await User_RoleRepo.DeleteRelation(ID);
            return Ok();
        }
    }
}
