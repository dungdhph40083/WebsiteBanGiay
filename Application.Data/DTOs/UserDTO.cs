using Application.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class UserDTO
    {
        [MaxLength(30)]
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Guid? ImageID { get; set; }
        public string? Address { get; set; }
        [MaxLength(30)]
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public byte Status { get; set; }
    }
}
