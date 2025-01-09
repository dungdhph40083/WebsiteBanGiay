namespace Application.Data.DTOs
{
    public class CustomerSupportMessageDTO
    {
        public Guid MessageID { get; set; }
        public string? MessageContent { get; set; }
        public Guid? UserID { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public byte? Status { get; set; }

    }
}
