using Application.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.Models
{
	public class Size
	{
		[Key]
		public Guid SizeID { get; set; }
		public string? Name { get; set; }
		public byte? Status { get; set; }
		public DateTime CreatedAt {  get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
