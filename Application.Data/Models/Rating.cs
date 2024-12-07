using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Rating
	{
		[Key]
		public Guid RatingID { get; set; }
		[ForeignKey(nameof(User))]
		public Guid? UserID { get; set; }
		[ForeignKey(nameof(Product))]
		public Guid? ProductID { get; set; }
		[Precision(2, 1)]
		public decimal? Stars {  get; set; }
		public string? Comment { get; set; }
		public DateTime? CreatedAt { get; set; }
		public virtual User? User { get; set; }
		public virtual Product? Product { get; set; }
	}
}
