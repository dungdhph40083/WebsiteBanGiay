using Microsoft.EntityFrameworkCore;

namespace Application.Data.DTOs
{
    public class RatingsDTO
    {
        public Guid? UserID { get; set; }
        public Guid? ProductID { get; set; }
        [Precision(2, 1)]
        public decimal? Stars { get; set; }
        public string? Comment { get; set; }
    }
}
