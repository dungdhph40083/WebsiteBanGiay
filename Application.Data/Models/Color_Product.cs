using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Color_Product
    {
        [                    Key                    ]
        public Guid Color_ProductID { get; set; }


        [         ForeignKey(nameof(Color))         ]
        public Guid? ColorID { get; set; }


        [        ForeignKey(nameof(Product))        ]
        public Guid? ProductID { get; set; }

        public bool Available { get; set; }

        public Color? Color { get; set; }
        public Product? Product { get; set; }
    }
}
