using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Color_ProductDetail
	{
		[Key]
		public Guid Color_ProductDetailID { get; set; }
		[ForeignKey(nameof(Color))]
		public Guid? ColorID { get; set; }
        [ForeignKey(nameof(ProductDetail))]
        public Guid? ProductDetailID { get; set; }

		public virtual Color? Color { get; set; }
		public virtual ProductDetail? ProductDetail { get; set; }
	}
}
