using Application.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> Options) : base(Options)
        {

        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Category_Product> Category_Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<CustomerSupportTickets> CustomerSupportTickets { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderTracking> OrderTrackings { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentMethodDetail> PaymentMethodDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Details_Color> Product_Details_Colors { get; set; }
        public DbSet<Product_Details_Size> Product_Details_Sizes { get; set; }
        public DbSet<Product_Inventory> Product_Inventorys { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
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

        protected override void OnConfiguring(DbContextOptionsBuilder OptionsBuilder)
        {
            base.OnConfiguring(OptionsBuilder);
        }
    }
}
