using System.ComponentModel.DataAnnotations;

namespace Application.MVC.GeneralPublic.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Bắt buộc.")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Bắt buộc.")]
        public string Password { get; set; } = null!;
    }
}
