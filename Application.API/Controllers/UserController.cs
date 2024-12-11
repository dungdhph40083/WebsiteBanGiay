using Application.API.Models;
using Application.API.Service;
using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser UserRepo;
        private readonly IImageRepository ImageRepo;
        public UserController(IUser UserRepo, IImageRepository ImageRepo)
        {
            this.UserRepo = UserRepo;
            this.ImageRepo = ImageRepo;
        }

        // lấy hết
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return await UserRepo.GetUsers();
        }

        // lấy theo ID (1 cái)
        [HttpGet("{ID}")]
        public async Task<ActionResult<User?>> Get(Guid ID)
        {
            return await UserRepo.GetUserByID(ID);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserSession?>> Login([FromForm] LoginDTO LoginInfo)
        {
            var IsValid = await UserRepo.ValidAccount(LoginInfo.Username, LoginInfo.Password);
            if (IsValid != null)
            {
                return new UserSession()
                {
                    UserID = IsValid,
                    Username = LoginInfo.Username,
                    JWToken = "Not_Yet"
                };
            }
            else return NotFound(null);
        }

        // tạo mới người dùng
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromForm] UserDTO NewUser, IFormFile? ProfilePic)
        {
            try
            {
                if (ProfilePic != null)
                {
                    switch (ImageUploaderValidator.ValidateImageSizeAndHeader(ProfilePic, 4_194_304))
                    {
                        case ErrorResult.IMAGE_TOO_BIG_ERROR:
                            return BadRequest($"Tệp ảnh không được vượt quá {4_194_304 / 1_048_576}MB!");
                        case ErrorResult.IMAGE_IS_BROKEN_ERROR:
                        default:
                            return BadRequest("Tệp ảnh không hợp lệ!");
                        case SuccessResult.IMAGE_OK:
                            {
                                var CreatedImage = await ImageRepo.CreateImageAsync(ProfilePic);
                                NewUser.ImageID = CreatedImage.ImageID;
                            }
                            break;
                    }
                }
                var Response = await UserRepo.CreateUser(NewUser);
                return CreatedAtAction(nameof(Get), new { ID = Response.UserID }, Response);
            }
            catch (Exception Exc)
            {
                if (Exc.HResult == -2147467261)
                {
                    var Errors = new ErrorsClass()
                    {
                        Password =
                        [
                            "Mật khẩu không được để trống."
                        ]
                    };

                    var ErrorMsg = new ResponseBodyMimic
                    {
                        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                        Title = "Ome or more validation errors occurred.",
                        Status = 400,
                        Errors = Errors,
                        TraceId = "THERE_IS_NO_TRACE_ID_ONLY_A_CHANGE_OF_WORLDS"
                    };

                    return StatusCode(400, ErrorMsg);
                }
                else throw;
            }
        }

        // cập nhật người dùng (1 cái)
        [HttpPut("{ID}")]
        public async Task<ActionResult<User?>> Put(Guid ID, [FromForm] UserDTO UpdatedUser, IFormFile? NewProfilePic)
        {
            if (NewProfilePic != null)
            {
                switch (ImageUploaderValidator.ValidateImageSizeAndHeader(NewProfilePic, 4_194_304))
                {
                    case ErrorResult.IMAGE_TOO_BIG_ERROR:
                        return BadRequest($"Tệp ảnh không được vượt quá {4_194_304 / 1_048_576}MB!");
                    case ErrorResult.IMAGE_IS_BROKEN_ERROR:
                    default:
                        return BadRequest("Tệp ảnh không hợp lệ!");
                    case SuccessResult.IMAGE_OK:
                        {
                            var CreatedImage = await ImageRepo.CreateImageAsync(NewProfilePic);
                            UpdatedUser.ImageID = CreatedImage.ImageID;
                        }
                        break;
                }
            }
            return await UserRepo.UpdateUser(ID, UpdatedUser);
        }

        // cập nhật người dùng (1 cái) (2)
        [HttpPatch("Toggle/{ID}")]
        public async Task<ActionResult> ToggleUser(Guid ID)
        {
            bool Result = await UserRepo.ToggleUser(ID);
            if (Result) return Ok("SUCCESS");
            else return BadRequest("FAILURE");
        }

        // ko dùng nhưng cần thiết để xóa mấy cái người dùng ma đi
        [HttpDelete("{ID}")]
        public async Task<ActionResult> DeleteUser(Guid ID)
        {
            await UserRepo.DeleteUser(ID);
            return NoContent();
        }
    }
}
