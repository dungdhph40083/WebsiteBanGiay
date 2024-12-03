using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.Models
{
	public class Image
	{
		[Key]
        [Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
        public Guid ImageID { get; set; }
		public string? ImageName { get; set; }
		public string? ImageDescription { get; set; }
		public string? ImageFileName { get; set; }
        [Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
        public byte Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
