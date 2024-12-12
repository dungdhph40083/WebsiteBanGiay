using Application.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;
        public LoginController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] AuthRequest request)
        {
            try
            {
                var user = _userService.Register(request.Username, request.Password);
                return Ok(new
                {
                    user.UserID, 
                    user.Username
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            var token = _userService.Authenticate(request.Username, request.Password);
            if (token == null) return Unauthorized(new { message = "Invalid credentials" });

            return Ok(new AuthResponse
            {
                Username = request.Username,
                Token = token
            });
        }
    }
}
