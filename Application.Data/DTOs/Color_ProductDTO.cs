using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Color_ProductDTO
    {
        public Guid? ColorID { get; set; }
        public Guid? ProductID { get; set; }
        public bool Available { get; set; }
    }
}
