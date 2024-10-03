﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Sizes
	{
		[Key]
		public Guid SizeID { get; set; }
		public string? SizeName { get; set; }
		public byte Status { get; set; }
		public DateTime? CreateDate {  get; set; }
		public DateTime? UpdateDate { get; set;}

	}
}
