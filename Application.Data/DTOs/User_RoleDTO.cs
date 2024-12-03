using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class User_RoleDTO
    {
        [Required(ErrorMessage = "Required.")]
        public Guid UserID { get; set; }
        [Required(ErrorMessage = "Required.")]
        public Guid RoleID { get; set; }
    }
}
