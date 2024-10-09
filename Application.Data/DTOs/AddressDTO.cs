using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.DTOs
{
    public class AddressDTO
    {
            public Guid AddressID { get; set; }
            public Guid UserID { get; set; }
            public Guid RoleID { get; set; }
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Description { get; set; }
            public string? DefaultAddress { get; set; }
            public byte Status { get; set; }
    }
}
