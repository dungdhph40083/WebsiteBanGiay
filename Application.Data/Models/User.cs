using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        [MaxLength(30)]
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        [ForeignKey(nameof(Image))]
        public Guid? ImageID { get; set; }
        public string? Address { get; set; }
        [MaxLength(30)]
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public byte Status { get; set; }
        public virtual Image? Image { get; set; }
        public virtual ICollection<Role> Roles { get; set; } = [];
	}
}
