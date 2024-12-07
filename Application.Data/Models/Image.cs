using Application.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.Models
{
	public class Image
	{
		[Key]
        public Guid ImageID { get; set; }
		public string? ImageFileName { get; set; }
	}
}
