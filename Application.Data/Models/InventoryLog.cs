﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class InventoryLog
	{
		[Key]
		public Guid LogID { get; set; }
		[ForeignKey(nameof(Size))]
		public Guid? SizeID { get; set; }
		[ForeignKey(nameof(Color))]
		public Guid? ColorID { get; set; }
        [ForeignKey(nameof(ProductDetail))]
        public Guid? ProductDetailID { get; set; }
		public int QuantityInStock { get; set; }
		public byte Status { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public virtual Size? Size { get; set; }
		public virtual Color? Color { get; set; }
		public virtual ProductDetail? ProductDetail { get; set; }
	}
}
