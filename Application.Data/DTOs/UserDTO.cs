using Application.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Application.Data.Enums;

namespace Application.Data.DTOs
{
    public class UserDTO
    {
        [MinLength(3, ErrorMessage = "Username too short!")]
        [MaxLength(30, ErrorMessage = "Username too long!")]
        [Required(ErrorMessage = "Required.")]
        public string Username { get; set; } = null!;
        [MinLength(8, ErrorMessage = "Password too short.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W)[A-Za-z\d\W]{8,}$",
            ErrorMessage = "Password isn't secure enough!"
            )]
        [Required(ErrorMessage = "Required.")]
        public string Password { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email!")]
        public string? Email { get; set; }
        public Guid? ImageID { get; set; }
        public string? Address { get; set; }
        // 
        [Length(9, 30, ErrorMessage = "Phone num. too short/long!")]
        [RegularExpression(@"^\d*$", ErrorMessage = "Invalid phone number.")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Invalid.")]
        public byte Status { get; set; }

        /*
         * I don't know why I'm constantly getting déjà vu from just looking at this code
         * Or when doing something else...
         * Or when drawing anything...
         * Or when chatting...
         * Or when modding a game...
         * Or when just trying to rest...
         * Or when I'm figuring out why I'm having the weirdest déjà vus ever...
         * ...
         * Am I going crazy?
         */
    }
}
