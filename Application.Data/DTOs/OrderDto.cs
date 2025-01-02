using System.ComponentModel.DataAnnotations;

namespace Application.Data.DTOs
{
    public class OrderDto
    {
        public Guid? UserID { get; set; }
        public Guid? PaymentMethodID { get; set; }
        public Guid? VoucherID { get; set; }
        public bool HasExternalInfo { get; set; }
        [Required]
        [MinLength(1)]
        public string? FirstName { get; set; }
        [Required]
        [MinLength(1)]
        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [Length(0, 30)]
        public string? PhoneNumber { get; set; }
        [Required]
        [MinLength(1)]
        public string? ShippingAddress { get; set; }
    }
}
