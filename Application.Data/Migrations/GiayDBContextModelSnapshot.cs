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
                .HasAnnotation("ProductVersion", "9.0.0")
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

            modelBuilder.Entity("Application.Data.Models.Color", b =>
                {
                    b.Property<Guid>("ColorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ColorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ColorID");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Application.Data.Models.Color_Product", b =>
                {
                    b.Property<Guid>("Color_ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Available")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<Guid?>("ColorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Color_ProductID");

                    b.HasIndex("ColorID");

                    b.HasIndex("ProductID");

                    b.ToTable("Color_Products");
                });

            modelBuilder.Entity("Application.Data.Models.CustomerSupportMessage", b =>
                {
                    b.Property<Guid>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("MessageID");

                    b.HasIndex("UserID");

                    b.ToTable("CustomerSupportMessages");
                });

            modelBuilder.Entity("Application.Data.Models.Image", b =>
                {
                    b.Property<Guid>("ImageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageFileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Application.Data.Models.Order", b =>
                {
                    b.Property<Guid>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AcceptEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("AcceptStart")
                        .HasColumnType("datetime2");

                    b.Property<byte>("AttemptsLeft")
                        .HasColumnType("tinyint");

                    b.Property<long?>("DiscountValue")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("GrandTotal")
                        .HasColumnType("bigint");

                    b.Property<bool>("HasChangedInfo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("HasExternalInfo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("HasPaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("PaymentMethodID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("RawTotal")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("RefundEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RefundStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShippingAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasDefaultValue((byte)100);

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("VoucherID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderID");

                    b.HasIndex("OrderNumber")
                        .IsUnique()
                        .HasFilter("[OrderNumber] IS NOT NULL");

                    b.HasIndex("PaymentMethodID");

                    b.HasIndex("UserID");

                    b.HasIndex("VoucherID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Application.Data.Models.OrderDetail", b =>
                {
                    b.Property<Guid>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("Discount")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("Price")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("ProductDetailID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("SaleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("SumTotalPrice")
                        .HasColumnType("bigint");

                    b.Property<long?>("TotalUnitPrice")
                        .HasColumnType("bigint");

                    b.HasKey("OrderDetailID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductDetailID");

                    b.HasIndex("SaleID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Application.Data.Models.OrderTracking", b =>
                {
                    b.Property<Guid>("TrackingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("HasPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LogDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint");

                    b.HasKey("TrackingID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderTrackings");
                });

            modelBuilder.Entity("Application.Data.Models.PaymentMethod", b =>
                {
                    b.Property<Guid>("PaymentMethodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MethodName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("UpdatedAt")
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

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint");

                    b.Property<long?>("TotalMoney")
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

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ImageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("Price")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedAt")
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

                    b.Property<Guid?>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ColorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Material")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlaceOfOrigin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("SizeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ProductDetailID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("ColorID");

                    b.HasIndex("ProductID")
                        .IsUnique()
                        .HasFilter("[ProductID] IS NOT NULL");

                    b.HasIndex("SizeID");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("Application.Data.Models.ProductWarranty", b =>
                {
                    b.Property<Guid>("WarrantyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("WarrantyConditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("WarrantyPeriod")
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

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal?>("Stars")
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

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

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<byte?>("Status")
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

                    b.Property<Guid?>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DiscountPrice")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EndingAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("StartingAt")
                        .HasColumnType("datetime2");

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("SaleID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("ProductID");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Application.Data.Models.ShippingMethod", b =>
                {
                    b.Property<Guid>("ShippingMethodID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("EstimatedDeliveryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MethodName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ShippingFee")
                        .HasColumnType("bigint");

                    b.HasKey("ShippingMethodID");

                    b.ToTable("ShippingMethods");
                });

            modelBuilder.Entity("Application.Data.Models.ShoppingCart", b =>
                {
                    b.Property<Guid>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long?>("Price")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("ProductDetailID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("QuantityCart")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CartID");

                    b.HasIndex("ProductDetailID");

                    b.HasIndex("UserID");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Application.Data.Models.Size", b =>
                {
                    b.Property<Guid>("SizeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("SizeID");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("Application.Data.Models.Size_Product", b =>
                {
                    b.Property<Guid>("Size_ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Available")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SizeID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Size_ProductID");

                    b.HasIndex("ProductID");

                    b.HasIndex("SizeID");

                    b.ToTable("Size_Products");
                });

            modelBuilder.Entity("Application.Data.Models.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ImageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("392d4ba7-020c-4515-863e-5f6582f74b53"));

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint");

                    b.Property<string>("TokenResetPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TokenResetPasswordExpireTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("VoucherID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserID");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("ImageID");

                    b.HasIndex("RoleID");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.HasIndex("VoucherID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Application.Data.Models.Voucher", b =>
                {
                    b.Property<Guid>("VoucherID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("DiscountPercent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long?>("DiscountPrice")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EndingAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("RequiredGrandTotal")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("StartingAt")
                        .HasColumnType("datetime2");

                    b.Property<byte?>("Status")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("UseDiscountPrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int?>("UsesLeft")
                        .HasColumnType("int");

                    b.Property<string>("VoucherCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VoucherID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("Application.Data.Models.Color_Product", b =>
                {
                    b.HasOne("Application.Data.Models.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorID");

                    b.HasOne("Application.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.Navigation("Color");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Application.Data.Models.CustomerSupportMessage", b =>
                {
                    b.HasOne("Application.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Application.Data.Models.Order", b =>
                {
                    b.HasOne("Application.Data.Models.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodID");

                    b.HasOne("Application.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.HasOne("Application.Data.Models.Voucher", "Voucher")
                        .WithMany()
                        .HasForeignKey("VoucherID");

                    b.Navigation("PaymentMethod");

                    b.Navigation("User");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Application.Data.Models.OrderDetail", b =>
                {
                    b.HasOne("Application.Data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID");

                    b.HasOne("Application.Data.Models.ProductDetail", "ProductDetail")
                        .WithMany()
                        .HasForeignKey("ProductDetailID");

                    b.HasOne("Application.Data.Models.Sale", "Sale")
                        .WithMany()
                        .HasForeignKey("SaleID");

                    b.Navigation("Order");

                    b.Navigation("ProductDetail");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("Application.Data.Models.OrderTracking", b =>
                {
                    b.HasOne("Application.Data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID");

                    b.Navigation("Order");
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
                    b.HasOne("Application.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID");

                    b.HasOne("Application.Data.Models.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorID");

                    b.HasOne("Application.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.HasOne("Application.Data.Models.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeID");

                    b.Navigation("Category");

                    b.Navigation("Color");

                    b.Navigation("Product");

                    b.Navigation("Size");
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
                    b.HasOne("Application.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID");

                    b.HasOne("Application.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Application.Data.Models.ShoppingCart", b =>
                {
                    b.HasOne("Application.Data.Models.ProductDetail", "ProductDetail")
                        .WithMany()
                        .HasForeignKey("ProductDetailID");

                    b.HasOne("Application.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("ProductDetail");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Application.Data.Models.Size_Product", b =>
                {
                    b.HasOne("Application.Data.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.HasOne("Application.Data.Models.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeID");

                    b.Navigation("Product");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Application.Data.Models.User", b =>
                {
                    b.HasOne("Application.Data.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID");

                    b.HasOne("Application.Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID");

                    b.HasOne("Application.Data.Models.Voucher", "Voucher")
                        .WithMany()
                        .HasForeignKey("VoucherID");

                    b.Navigation("Image");

                    b.Navigation("Role");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Application.Data.Models.Voucher", b =>
                {
                    b.HasOne("Application.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID");

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
