using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Vouchers
	{
		[Key]
		public Guid VoucherID { get; set; }
		public Guid CategoryID { get; set; }
		public Guid ProductID {  get; set; }
		public decimal DiscountPrice { get; set; }
		public decimal DiscountPerson { get; set;}
		public string? Description { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime LastUpdatedOn { get; set;}
		public byte Status { get; set; }
		
	}
}
