using Application.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Data.Models
{
	public class Size
	{
		[Key]
		[Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
		public Guid SizeID { get; set; }
		public string? Name { get; set; }
		[Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
		public byte Status { get; set; }
		public DateTime CreatedAt {  get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
