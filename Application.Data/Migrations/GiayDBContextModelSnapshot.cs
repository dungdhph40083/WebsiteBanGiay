﻿// <auto-generated />
using System;
using Application.Data.ModelContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Application.Data.Migrations
{
    [DbContext(typeof(GiayDBContext))]
    partial class GiayDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Application.Data.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Application.Data.Models.Category_Product", b =>
                {
                    b.Property<Guid?>("Category_Products_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Category_Products_ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("ProductID");

                    b.ToTable("Category_Products");
                });

            modelBuilder.Entity("Application.Data.Models.Color", b =>
                {
                    b.Property<Guid>("ColorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ColorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ColorID");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Application.Data.Models.Color_ProductDetail", b =>
                {
                    b.Property<Guid>("Color_ProductDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ColorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductDetailID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Color_ProductDetailID");

                    b.HasIndex("ColorID");

                    b.HasIndex("ProductDetailID");

                    b.ToTable("Color_ProductDetails");
                });

            modelBuilder.Entity("Application.Data.Models.CustomerSupportMessage", b =>
                {
                    b.Property<long>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("MessageID"));

                    b.Property<Guid?>("CustomerSupportTicketTicketID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MessageContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TicketID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MessageID");

                    b.HasIndex("CustomerSupportTicketTicketID");

                    b.ToTable("CustomerSupportMessages");
                });

            modelBuilder.Entity("Application.Data.Models.CustomerSupportTicket", b =>
                {
                    b.Property<Guid>("TicketID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TicketID");

                    b.HasIndex("UserID");

                    b.ToTable("CustomerSupportTickets");
                });

            modelBuilder.Entity("Application.Data.Models.Image", b =>
                {
                    b.Property<Guid>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ImageID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Application.Data.Models.InventoryLog", b =>
                {
                    b.Property<Guid>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ColorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductDetailID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<Guid?>("SizeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("LogID");

                    b.HasIndex("ColorID");

                    b.HasIndex("ProductDetailID");

                    b.HasIndex("SizeID");

                    b.ToTable("InventoryLogs");
                });

            modelBuilder.Entity("Application.Data.Models.Inventory_ProductDetail", b =>
                {
                    b.Property<Guid>("Inventory_ProductDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LogID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductDetailsID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Inventory_ProductDetailID");

                    b.HasIndex("LogID");

                    b.HasIndex("ProductDetailsID");

                    b.ToTable("Inventory_ProductDetails");
                });

            modelBuilder.Entity("Application.Data.Models.Order", b =>
                {
                    b.Property<Guid>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("PaymentMethodID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ShippingAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderID");

                    b.HasIndex("PaymentMethodID");

                    b.HasIndex("UserID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Application.Data.Models.OrderDetail", b =>
                {
                    b.Property<Guid>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ColorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<Guid?>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("ShippingMethodID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SizeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("SumTotalPrice")
                        .HasColumnType("bigint");

                    b.Property<long>("TotalUnitPrice")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("VoucherID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderDetailID");

                    b.HasIndex("ColorID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.HasIndex("ShippingMethodID");

                    b.HasIndex("SizeID");

                    b.HasIndex("VoucherID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Application.Data.Models.OrderTracking", b =>
                {
                    b.Property<Guid>("TrackingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderDetailID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("ShippingStatus")
                        .HasColumnType("tinyint");

                    b.Property<long>("TrackingNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("TrackingID");

                    b.HasIndex("OrderDetailID");

                    b.ToTable("OrderTrackings");
                });

            modelBuilder.Entity("Application.Data.Models.PaymentMethod", b =>
                {
                    b.Property<Guid>("PaymentMethodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MethodName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("PaymentMethodID");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("Application.Data.Models.PaymentMethodDetail", b =>
                {
                    b.Property<Guid>("PaymentMethodDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PaymentMethodID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<long>("TotalMoney")
                        .HasColumnType("bigint");

                    b.HasKey("PaymentMethodDetailID");

                    b.HasIndex("PaymentMethodID");

                    b.ToTable("PaymentMethodDetails");
                });

            modelBuilder.Entity("Application.Data.Models.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ImageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductID");

                    b.HasIndex("ImageID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Application.Data.Models.ProductDetail", b =>
                {
                    b.Property<Guid>("ProductDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Features")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ImageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Instructions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Material")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfOrigin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Price")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WarrantyPeriod")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductDetailID");

                    b.HasIndex("ImageID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("Application.Data.Models.ProductWarranty", b =>
                {
                    b.Property<Guid>("WarrantyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("WarrantyConditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WarrantyPeriod")
                        .HasColumnType("datetime2");

                    b.HasKey("WarrantyID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductWarranties");
                });

            modelBuilder.Entity("Application.Data.Models.Rating", b =>
                {
                    b.Property<Guid>("RatingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateRated")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Stars")
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RatingID");

                    b.HasIndex("ProductID");

                    b.HasIndex("UserID");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Application.Data.Models.Return", b =>
                {
                    b.Property<Guid>("ReturnID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RefundAmount")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("ReturnID");

                    b.HasIndex("OrderID");

                    b.ToTable("Returns");
                });

            modelBuilder.Entity("Application.Data.Models.Role", b =>
                {
                    b.Property<Guid>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Application.Data.Models.Sale", b =>
                {
                    b.Property<Guid>("SaleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SaleCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("SaleID");

                    b.HasIndex("ProductID");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Application.Data.Models.ShippingMethod", b =>
                {
                    b.Property<Guid>("ShippingMethodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EstimatedDeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MethodName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ShippingFee")
                        .HasColumnType("bigint");

                    b.HasKey("ShippingMethodID");

                    b.ToTable("ShippingMethods");
                });

            modelBuilder.Entity("Application.Data.Models.ShoppingCart", b =>
                {
                    b.Property<Guid>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ColorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<long>("Discount")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsCheckedOut")
                        .HasColumnType("bit");

                    b.Property<long?>("Price")
                        .HasColumnType("bigint");

                    b.Property<int>("QuantityCart")
                        .HasColumnType("int");

                    b.Property<Guid?>("SizeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("VoucherID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CartID");

                    b.HasIndex("ColorID");

                    b.HasIndex("SizeID");

                    b.HasIndex("UserID");

                    b.HasIndex("VoucherID");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Application.Data.Models.Size", b =>
                {
                    b.Property<Guid>("SizeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("SizeID");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("Application.Data.Models.Size_ProductDetail", b =>
                {
                    b.Property<Guid>("Size_ProductDetail_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SizeID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Size_ProductDetail_ID");

                    b.HasIndex("ProductID");

                    b.HasIndex("SizeID");

                    b.ToTable("Size_ProductDetails");
                });

            modelBuilder.Entity("Application.Data.Models.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ImageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("UserID");

                    b.HasIndex("ImageID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Application.Data.Models.User_Role", b =>
                {
                    b.Property<Guid>("User_RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("User_RoleID");

                    b.HasIndex("RoleID");

                    b.HasIndex("UserID");

                    b.ToTable("User_Roles");
                });

            modelBuilder.Entity("Application.Data.Models.Voucher", b =>
                {
                    b.Property<Guid>("VoucherID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DiscountPercent")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<long>("DiscountPrice")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LastUpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("VoucherID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("ProductID");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("Application.Data.Models.Category_Product", b =>
                {
                    b.HasOne("Application.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID");

                    b.HasOne("Application.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Application.Data.Models.Color_ProductDetail", b =>
                {
                    b.HasOne("Application.Data.Models.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorID");

                    b.HasOne("Application.Data.Models.ProductDetail", "ProductDetail")
                        .WithMany()
                        .HasForeignKey("ProductDetailID");

                    b.Navigation("Color");

                    b.Navigation("ProductDetail");
                });

            modelBuilder.Entity("Application.Data.Models.CustomerSupportMessage", b =>
                {
                    b.HasOne("Application.Data.Models.CustomerSupportTicket", null)
                        .WithMany("CustomerSupportMessages")
                        .HasForeignKey("CustomerSupportTicketTicketID");
                });

            modelBuilder.Entity("Application.Data.Models.CustomerSupportTicket", b =>
                {
                    b.HasOne("Application.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Application.Data.Models.InventoryLog", b =>
                {
                    b.HasOne("Application.Data.Models.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorID");

                    b.HasOne("Application.Data.Models.ProductDetail", "ProductDetail")
                        .WithMany()
                        .HasForeignKey("ProductDetailID");

                    b.HasOne("Application.Data.Models.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeID");

                    b.Navigation("Color");

                    b.Navigation("ProductDetail");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Application.Data.Models.Inventory_ProductDetail", b =>
                {
                    b.HasOne("Application.Data.Models.InventoryLog", "InventoryLog")
                        .WithMany()
                        .HasForeignKey("LogID");

                    b.HasOne("Application.Data.Models.ProductDetail", "ProductDetail")
                        .WithMany()
                        .HasForeignKey("ProductDetailsID");

                    b.Navigation("InventoryLog");

                    b.Navigation("ProductDetail");
                });

            modelBuilder.Entity("Application.Data.Models.Order", b =>
                {
                    b.HasOne("Application.Data.Models.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodID");

                    b.HasOne("Application.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("PaymentMethod");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Application.Data.Models.OrderDetail", b =>
                {
                    b.HasOne("Application.Data.Models.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorID");

                    b.HasOne("Application.Data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID");

                    b.HasOne("Application.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.HasOne("Application.Data.Models.ShippingMethod", "ShippingMethod")
                        .WithMany()
                        .HasForeignKey("ShippingMethodID");

                    b.HasOne("Application.Data.Models.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeID");

                    b.HasOne("Application.Data.Models.Voucher", "Voucher")
                        .WithMany()
                        .HasForeignKey("VoucherID");

                    b.Navigation("Color");

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("ShippingMethod");

                    b.Navigation("Size");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Application.Data.Models.OrderTracking", b =>
                {
                    b.HasOne("Application.Data.Models.OrderDetail", "OrderDetail")
                        .WithMany()
                        .HasForeignKey("OrderDetailID");

                    b.Navigation("OrderDetail");
                });

            modelBuilder.Entity("Application.Data.Models.PaymentMethodDetail", b =>
                {
                    b.HasOne("Application.Data.Models.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodID");

                    b.Navigation("PaymentMethod");
                });

            modelBuilder.Entity("Application.Data.Models.Product", b =>
                {
                    b.HasOne("Application.Data.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Application.Data.Models.ProductDetail", b =>
                {
                    b.HasOne("Application.Data.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID");

                    b.HasOne("Application.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.Navigation("Image");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Application.Data.Models.ProductWarranty", b =>
                {
                    b.HasOne("Application.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Application.Data.Models.Rating", b =>
                {
                    b.HasOne("Application.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.HasOne("Application.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Application.Data.Models.Return", b =>
                {
                    b.HasOne("Application.Data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Application.Data.Models.Sale", b =>
                {
                    b.HasOne("Application.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Application.Data.Models.ShoppingCart", b =>
                {
                    b.HasOne("Application.Data.Models.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorID");

                    b.HasOne("Application.Data.Models.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeID");

                    b.HasOne("Application.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.HasOne("Application.Data.Models.Voucher", "Voucher")
                        .WithMany()
                        .HasForeignKey("VoucherID");

                    b.Navigation("Color");

                    b.Navigation("Size");

                    b.Navigation("User");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Application.Data.Models.Size_ProductDetail", b =>
                {
                    b.HasOne("Application.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Data.Models.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Application.Data.Models.User", b =>
                {
                    b.HasOne("Application.Data.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Application.Data.Models.User_Role", b =>
                {
                    b.HasOne("Application.Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Application.Data.Models.User", "User")
                        .WithMany("User_Roles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Application.Data.Models.Voucher", b =>
                {
                    b.HasOne("Application.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID");

                    b.HasOne("Application.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Application.Data.Models.CustomerSupportTicket", b =>
                {
                    b.Navigation("CustomerSupportMessages");
                });

            modelBuilder.Entity("Application.Data.Models.User", b =>
                {
                    b.Navigation("User_Roles");
                });
#pragma warning restore 612, 618
        }
    }
}