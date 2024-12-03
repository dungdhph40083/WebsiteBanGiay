using Application.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models
{
	public class Size_ProductDetail
	{
		[Key]
		[Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
		public Guid Size_ProductDetail_ID { get; set; }
		[ForeignKey(nameof(ProductDetail))]
        [Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
        public Guid ProductDetailID { get; set; }
		[ForeignKey(nameof(Size))]
        [Required(ErrorMessage = ValidateErrorResult.EMPTY_FIELD_NOT_ALLOWED)]
        public Guid SizeID { get; set; }

		public virtual ProductDetail? ProductDetail { get; set; }
		public virtual Size? Size { get; set; }
	}			
}
