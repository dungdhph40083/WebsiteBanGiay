﻿using Application.API.Models;
using Application.API.Service;
using Application.Data.DTOs;
using Application.Data.Enums;
using Application.Data.ModelContexts;
using Application.Data.Models;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly GiayDBContext _giayDBContext;
        private readonly IUser UserRepo;
        private readonly IImageRepository ImageRepo;
        private readonly IEmailService _emailService;
        private readonly IMapper Mapper;
        public UserController(IUser UserRepo, IImageRepository ImageRepo, GiayDBContext giayDBContext, IEmailService emailService, IMemoryCache memoryCache, IMapper Mapper)
        {
            this.UserRepo = UserRepo;
            this.ImageRepo = ImageRepo;
            this._giayDBContext = giayDBContext;
            this._emailService = emailService;
            this._memoryCache = memoryCache;
            this.Mapper = Mapper;
        }

        // lấy hết
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<User>>> Get()
        {
            return await UserRepo.GetUsers();
        }

        // lấy theo ID (1 cái)
        [HttpGet("{ID}")]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post([FromForm] UserDTO NewUser, IFormFile? ProfilePic)
        {
            var Sfdsjhhsdl = await UserRepo.UsernameChecker(NewUser.Username);
            if (!Sfdsjhhsdl) return Conflict("USERNAME_CONFLICT");
            var Bjsidjfprt = await UserRepo.EmailChecker(NewUser.Email);
            if (!Bjsidjfprt) return Conflict("EMAIL_CONFLICT");
            var Pewrifsosd = await UserRepo.PhoneNumberChecker(NewUser.PhoneNumber);
            if (!Pewrifsosd) return Conflict("PHONE_NUM_CONFLICT");

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
        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDTO NewUser)
        {
            try
            {
                NewUser.RoleID = Guid.Parse(DefaultValues.UserRoleGUID);
                NewUser.IsBanned = DefaultValues.IsBanned;
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

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _giayDBContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return NotFound(new { message = "Email không tồn tại." });
            }

            var token = Guid.NewGuid().ToString();
            _memoryCache.Set(token, user.Email, TimeSpan.FromHours(1));
            var resetLink = $"https://localhost:7282/Login/ResetPassword?token={token}";
            await _emailService.SendEmailAsync(user.Email, "Reset Password",
                $"Click vào đây để đặt lại mật khẩu: <a href='{resetLink}'>link</a>");

            return Ok(new { message = "Email đặt lại mật khẩu đã được gửi." });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var email = _memoryCache.Get<string>(model.Token);
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { message = "Token không hợp lệ hoặc đã hết hạn." });
            }

            var user = await _giayDBContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return NotFound(new { message = "Người dùng không tồn tại." });
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            user.Password = model.NewPassword;
            await _giayDBContext.SaveChangesAsync();

            _memoryCache.Remove(model.Token);

            return Ok(new { message = "Mật khẩu đã được đặt lại thành công." });
        }
        [HttpPost("ban/{userId}")]
        public async Task<IActionResult> BanAccount(Guid userId)
        {
            var user = await _giayDBContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.IsBanned = true;
            await _giayDBContext.SaveChangesAsync();
            return Ok("Account has been banned.");
        }

        [HttpPost("unban/{userId}")]
        public async Task<IActionResult> UnbanAccount(Guid userId)
        {
            var user = await _giayDBContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            user.IsBanned = false;
            await _giayDBContext.SaveChangesAsync();
            return Ok("Account has been unbanned.");
        }


        [HttpPut("{ID}")]
        public async Task<ActionResult<User?>> Put(Guid ID, [FromForm] UserEditDTO UpdatedUserDTO, IFormFile? NewProfilePic)
        {
            var OldUser = await UserRepo.GetUserByID(ID);
            if (!string.Equals(OldUser?.Username, UpdatedUserDTO.Username, StringComparison.OrdinalIgnoreCase))
            {
                var Sfdsjhhsdl = await UserRepo.UsernameChecker(UpdatedUserDTO.Username);
                if (!Sfdsjhhsdl) return Conflict("USERNAME_CONFLICT");
            }
            if (!string.Equals(OldUser?.Email, UpdatedUserDTO?.Email, StringComparison.OrdinalIgnoreCase))
            {
                var Bjsidjfprt = await UserRepo.EmailChecker(UpdatedUserDTO?.Email);
                if (!Bjsidjfprt) return Conflict("EMAIL_CONFLICT");
            }
            if (!string.Equals(OldUser?.PhoneNumber, UpdatedUserDTO?.PhoneNumber, StringComparison.OrdinalIgnoreCase))
            {
                var Pewrifsosd = await UserRepo.PhoneNumberChecker(UpdatedUserDTO?.PhoneNumber);
                if (!Pewrifsosd) return Conflict("PHONE_NUM_CONFLICT");
            }

            var UpdatedUser = new UserDTO();
            UpdatedUser = Mapper.Map(UpdatedUserDTO, UpdatedUser);

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

        // For debugging. Don't use.
        [HttpDelete("{ID}")]
        public async Task<ActionResult> DeleteUser(Guid ID)
        {
            try
            {
                await UserRepo.DeleteUser(ID);
                return NoContent();
            }
            catch (Exception)
            {
                var Target = await UserRepo.GetUserByID(ID);
                if (Target != null) return Conflict();
                else return NoContent();
            }
        }
    }
}
