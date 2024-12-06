using Application.Data.Enums;
using Application.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.ModelContexts
{
	public class GiayDBContext : DbContext
	{
        public GiayDBContext() { }
        public GiayDBContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

            var connectionString = configuration.GetConnectionString("DatabaseBanGiay");

            // Configure the context to use SQL Server with the connection string
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
			ModelBuilder.Entity<User>()
				.HasIndex(Idx => Idx.Username)
				.IsUnique();
            ModelBuilder.Entity<User>()
                .HasIndex(Idx => Idx.Email)
                .IsUnique();
			ModelBuilder.Entity<User>()
				.Property(Idx => Idx.RoleID)
				.HasDefaultValue(Guid.Parse(DefaultValues.UserRoleGUID));
        }

        public DbSet<Category> Categories { get; set; }
		public DbSet<Color> Colors { get; set; }
		public DbSet<CustomerSupportMessage> CustomerSupportMessages { get; set; }
		public DbSet<CustomerSupportTicket> CustomerSupportTickets { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<OrderTracking> OrderTrackings { get; set; }
		public DbSet<PaymentMethod> PaymentMethods { get; set; }
		public DbSet<PaymentMethodDetail> PaymentMethodDetails { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductDetail> ProductDetails { get; set; }
		public DbSet<ProductWarranty> ProductWarranties { get; set; }
		public DbSet<Rating> Ratings { get; set; }
		public DbSet<Return> Returns { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Sale> Sales { get; set; }
		public DbSet<ShippingMethod> ShippingMethods { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<Size> Sizes { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Voucher> Vouchers { get; set; }
	}
}
