using System.ComponentModel.DataAnnotations;

namespace Application.Data.Models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public byte[]? Avatar { get; set; }
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public byte Status { get; set; }
        public virtual Rating? Rating { get; set; }
    }
}
