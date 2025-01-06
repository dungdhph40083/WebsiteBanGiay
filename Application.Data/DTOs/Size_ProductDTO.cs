using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Size_ProductDTO
    {
        public Guid? SizeID { get; set; }
        public Guid? ProductID { get; set; }
    }
}
