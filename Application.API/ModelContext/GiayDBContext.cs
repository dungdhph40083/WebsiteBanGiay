using Application.Data.Models;
using Application.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.API.ModelContext
{
	public class GiayDBContext : DbContext
	{
		public GiayDBContext(DbContextOptions<GiayDBContext> Options) : base(Options)
		{
		}

	

		public DbSet<Category> Categories { get; set; }
		public DbSet<Category_Product> Category_Products { get; set; }
		public DbSet<Color> Colors { get; set; }
		public DbSet<Color_ProductDetail> Color_ProductDetails { get; set; }
		public DbSet<CustomerSupportMessage> CustomerSupportMessages { get; set; }
		public DbSet<CustomerSupportTicket> CustomerSupportTickets { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Inventory_ProductDetail> Inventory_ProductDetails { get; set; }
		public DbSet<InventoryLog> InventoryLogs { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<OrderTracking> OrderTrackings { get; set; }
		public DbSet<PaymentMethod> PaymentMethods { get; set; }
		public DbSet<PaymentMethodDetail> PaymentMethodDetails { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Color_ProductDetail> Product_Details_Colors { get; set; }
		public DbSet<Size_ProductDetail> Product_Details_Sizes { get; set; }
		public DbSet<Inventory_ProductDetail> Product_Inventorys { get; set; }
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
		public DbSet<User_Role> User_Roles { get; set; }
		public DbSet<Voucher> Vouchers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
	
	 //modelBuilder.Entity<Rating>()
		//.HasOne(r => r.User)
		//.WithMany(u => u.Ratings)
		//.HasForeignKey(r => r.UserID)
		//.OnDelete(DeleteBehavior.NoAction); // Sử dụng Restrict để tránh cascade delete
		//	modelBuilder.Entity<ProductReview>()
	 // .HasOne(pr => pr.User)
	 // .WithMany()
	 // .HasForeignKey(pr => pr.UserID)
	 // .OnDelete(DeleteBehavior.Restrict); // Không cho phép cascade delete

		//	modelBuilder.Entity<OrderDetail>()
	 //  .HasOne(od => od.Order)
	 //  .WithMany(o => o.OrderDetails)
	 //  .HasForeignKey(od => od.OrderID)
	 //  .OnDelete(DeleteBehavior.NoAction);


		}
	}
}
