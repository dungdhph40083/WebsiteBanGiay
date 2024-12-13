namespace Application.Data.DTOs
{
    public class CustomerSupportMessageDTO
    {
        public Guid MessageID { get; set; }
        public string? MessageContent { get; set; }
        public Guid? UserID { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
