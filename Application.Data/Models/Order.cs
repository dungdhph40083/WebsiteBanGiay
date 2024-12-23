﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.Models
{
    public class Order
    {
		[Key]
		public Guid OrderID { get; set; }
		[ForeignKey(nameof(User))]
		public Guid? UserID { get; set; }
        [ForeignKey(nameof(PaymentMethod))]
        public Guid? PaymentMethodID { get; set; }
        public DateTime? OrderDate { get; set; }
		public byte? Status { get; set; }
        public bool HasPaid { get; set; }
        public bool HasExternalInfo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Length(0, 30)]
        public string? PhoneNumber { get; set; }
        public string? ShippingAddress { get; set; }
		public long? GrandTotal { get; set; }
		public virtual User? User { get; set; }
		public virtual PaymentMethod? PaymentMethod { get; set; }
	}
}
