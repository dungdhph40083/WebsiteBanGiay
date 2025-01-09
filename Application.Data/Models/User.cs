using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        [ForeignKey(nameof(Role))]
        public Guid? RoleID { get; set; }
        [ForeignKey(nameof(Image))]
        public Guid? ImageID { get; set; }
        [ForeignKey(nameof(Voucher))]
        public Guid? VoucherID { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public byte? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Image? Image { get; set; }
        public virtual Voucher? Voucher { get; set; }
        public virtual Role? Role { get; set; }
	}

}
