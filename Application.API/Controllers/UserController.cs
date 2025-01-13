﻿using Application.API.Models;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Application.Data.ModelContexts;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Caching.Memory;

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
        public UserController(IUser UserRepo, IImageRepository ImageRepo, GiayDBContext giayDBContext, IEmailService emailService)
        {
            this.UserRepo = UserRepo;
            this.ImageRepo = ImageRepo;
            this._giayDBContext = giayDBContext;
            this._emailService = emailService;
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

        [HttpPost("api/forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _giayDBContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return NotFound(new { message = "Email không tồn tại." });
            }

            // Tạo token và lưu vào MemoryCache (hoặc Session)
            var token = Guid.NewGuid().ToString();
            _memoryCache.Set(token, user.Email, TimeSpan.FromHours(1)); // Lưu token tạm thời

            // Gửi email cho người dùng
            var resetLink = $"{Request.Scheme}://{Request.Host}/reset-password?token={token}";
            await _emailService.SendEmailAsync(user.Email, "Reset Password",
                $"Click vào đây để đặt lại mật khẩu: <a href='{resetLink}'>link</a>");

            return Ok(new { message = "Email đặt lại mật khẩu đã được gửi." });
        }


        [HttpPost("api/reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Kiểm tra token trong MemoryCache (hoặc Session)
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

            // Đặt lại mật khẩu
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            await _giayDBContext.SaveChangesAsync();

            // Xóa token sau khi hoàn tất
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
        [HttpDelete("{ID}")]
        public async Task<ActionResult> DeleteUser(Guid ID)
        {
            await UserRepo.DeleteUser(ID);
            return NoContent();
        }
        public class ForgotPasswordDto
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
        public class ResetPasswordDto
        {
            [Required]
            public string Token { get; set; }

            [Required]
            [MinLength(6, ErrorMessage = "Mật khẩu ít nhất 6 ký tự.")]
            public string NewPassword { get; set; }

            [Required]
            [Compare("NewPassword", ErrorMessage = "Mật khẩu không khớp.")]
            public string ConfirmPassword { get; set; }
        }


    }
}
