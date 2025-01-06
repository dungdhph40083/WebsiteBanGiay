using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Size_Product
    {
        [                    Key                    ]
        public Guid Size_ProductID { get; set; }

        [         ForeignKey(nameof(Size))          ]
        public Guid? SizeID { get; set; }

        [        ForeignKey(nameof(Product))        ]
        public Guid? ProductID { get; set; }

        public bool Available { get; set; }

        public Size? Size { get; set; }
        public Product? Product { get; set; }
    }
}
