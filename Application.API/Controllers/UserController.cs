using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser UserRepo;
        public UserController(IUser UserRepo)
        {
            this.UserRepo = UserRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return await UserRepo.GetUsers();
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<User?>> Get(Guid ID)
        {
            return await UserRepo.GetUserByID(ID);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] UserDTO NewUser)
        {
            return await UserRepo.CreateUser(NewUser);
        }

        [HttpPut("{ID}")]
        public async Task<ActionResult<User?>> Put(Guid ID, [FromBody] UserDTO UpdatedUser)
        {
            return await UserRepo.UpdateUser(ID, UpdatedUser);
        }

        [HttpDelete("{ID}")]
        public async Task<ActionResult> Delete(Guid ID)
        {
            await UserRepo.DeleteUser(ID);
            return Ok();
        }
    }
}
