namespace Application.API.Models
{
    public class ResponseBodyMimic
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int Status { get; set; }
        public ErrorsClass? Errors { get; set; }
        public string? TraceId { get; set; }
    }

    public class ErrorsClass
    {
        public List<string>? Password { get; set; }
    }
}
