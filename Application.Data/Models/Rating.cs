using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Rating
	{
		[Key]
		public int RatingID { get; set; }
		public Guid UserID { get; set; }
		public Guid ProductID { get; set; }
		public string? RatingStar {  get; set; }
		public string? Comment { get; set; }
		public DateTime? DateRated { get; set; }
	}
}
