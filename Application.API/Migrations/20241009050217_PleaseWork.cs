using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.API.Migrations
{
    /// <inheritdoc />
    public partial class PleaseWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorID);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "ShippingMethods",
                columns: table => new
                {
                    ShippingMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingFee = table.Column<long>(type: "bigint", nullable: false),
                    EstimatedDeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethods", x => x.ShippingMethodID);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ImageID");
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodDetails",
                columns: table => new
                {
                    PaymentMethodDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalMoney = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodDetails", x => x.PaymentMethodDetailID);
                    table.ForeignKey(
                        name: "FK_PaymentMethodDetails_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID");
                });

            migrationBuilder.CreateTable(
                name: "Category_Products",
                columns: table => new
                {
                    CategoryProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_Products", x => x.CategoryProductID);
                    table.ForeignKey(
                        name: "FK_Category_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK_Category_Products_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    ProductDetailsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShoeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.ProductDetailsID);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ImageID");
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "ProductWarranties",
                columns: table => new
                {
                    WarrantyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WarrantyPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarrantyConditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWarranties", x => x.WarrantyID);
                    table.ForeignKey(
                        name: "FK_ProductWarranties_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleID);
                    table.ForeignKey(
                        name: "FK_Sales_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    VoucherID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DiscountPrice = table.Column<long>(type: "bigint", nullable: false),
                    DiscountPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.VoucherID);
                    table.ForeignKey(
                        name: "FK_Vouchers_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK_Vouchers_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "InventoryLogs",
                columns: table => new
                {
                    LogID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ColorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuantityInStock = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLogs", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_InventoryLogs_Colors_ColorID",
                        column: x => x.ColorID,
                        principalTable: "Colors",
                        principalColumn: "ColorID");
                    table.ForeignKey(
                        name: "FK_InventoryLogs_Sizes_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Sizes",
                        principalColumn: "SizeID");
                });

            migrationBuilder.CreateTable(
                name: "CustomerSupportTickets",
                columns: table => new
                {
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSupportTickets", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_CustomerSupportTickets_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    TotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID");
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RatingStar = table.Column<decimal>(type: "decimal(1,1)", precision: 1, scale: 1, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingID);
                    table.ForeignKey(
                        name: "FK_Ratings_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                    table.ForeignKey(
                        name: "FK_Roles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Color_ProductDetail",
                columns: table => new
                {
                    Color_ProductDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color_ProductDetail", x => x.Color_ProductDetailID);
                    table.ForeignKey(
                        name: "FK_Color_ProductDetail_Colors_ColorID",
                        column: x => x.ColorID,
                        principalTable: "Colors",
                        principalColumn: "ColorID");
                    table.ForeignKey(
                        name: "FK_Color_ProductDetail_ProductDetails_ProductDetailID",
                        column: x => x.ProductDetailID,
                        principalTable: "ProductDetails",
                        principalColumn: "ProductDetailsID");
                });

            migrationBuilder.CreateTable(
                name: "Product_Details_Sizes",
                columns: table => new
                {
                    Size_ProductDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Details_Sizes", x => x.Size_ProductDetailID);
                    table.ForeignKey(
                        name: "FK_Product_Details_Sizes_ProductDetails_ProductDetailID",
                        column: x => x.ProductDetailID,
                        principalTable: "ProductDetails",
                        principalColumn: "ProductDetailsID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Details_Sizes_Sizes_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Sizes",
                        principalColumn: "SizeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    CartID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuantityCart = table.Column<int>(type: "int", nullable: false),
                    SizeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ColorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: true),
                    Discount = table.Column<long>(type: "bigint", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCheckedOut = table.Column<bool>(type: "bit", nullable: false),
                    VoucherID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.CartID);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Colors_ColorID",
                        column: x => x.ColorID,
                        principalTable: "Colors",
                        principalColumn: "ColorID");
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Sizes_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Sizes",
                        principalColumn: "SizeID");
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_Vouchers_VoucherID",
                        column: x => x.VoucherID,
                        principalTable: "Vouchers",
                        principalColumn: "VoucherID");
                });

            migrationBuilder.CreateTable(
                name: "Inventory_ProductDetail",
                columns: table => new
                {
                    Inventory_ProductDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductDetailsID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory_ProductDetail", x => x.Inventory_ProductDetailID);
                    table.ForeignKey(
                        name: "FK_Inventory_ProductDetail_InventoryLogs_LogID",
                        column: x => x.LogID,
                        principalTable: "InventoryLogs",
                        principalColumn: "LogID");
                    table.ForeignKey(
                        name: "FK_Inventory_ProductDetail_ProductDetails_ProductDetailsID",
                        column: x => x.ProductDetailsID,
                        principalTable: "ProductDetails",
                        principalColumn: "ProductDetailsID");
                });

            migrationBuilder.CreateTable(
                name: "CustomerSupportMessages",
                columns: table => new
                {
                    MessageID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerSupportTicketTicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSupportMessages", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_CustomerSupportMessages_CustomerSupportTickets_CustomerSupportTicketTicketID",
                        column: x => x.CustomerSupportTicketTicketID,
                        principalTable: "CustomerSupportTickets",
                        principalColumn: "TicketID");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SizeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ColorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VoucherID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShippingMethodID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    TotalUnitPrice = table.Column<long>(type: "bigint", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    SumTotalPrice = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailID);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Colors_ColorID",
                        column: x => x.ColorID,
                        principalTable: "Colors",
                        principalColumn: "ColorID");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_OrderDetails_ShippingMethods_ShippingMethodID",
                        column: x => x.ShippingMethodID,
                        principalTable: "ShippingMethods",
                        principalColumn: "ShippingMethodID");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Sizes_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Sizes",
                        principalColumn: "SizeID");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Vouchers_VoucherID",
                        column: x => x.VoucherID,
                        principalTable: "Vouchers",
                        principalColumn: "VoucherID");
                });

            migrationBuilder.CreateTable(
                name: "Returns",
                columns: table => new
                {
                    ReturnID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefundAmount = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Returns", x => x.ReturnID);
                    table.ForeignKey(
                        name: "FK_Returns_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "User_Roles",
                columns: table => new
                {
                    User_RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Roles", x => x.User_RoleID);
                    table.ForeignKey(
                        name: "FK_User_Roles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Roles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderTrackings",
                columns: table => new
                {
                    TrackingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TrackingNumber = table.Column<long>(type: "bigint", nullable: false),
                    ShippingStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTrackings", x => x.TrackingID);
                    table.ForeignKey(
                        name: "FK_OrderTrackings_OrderDetails_OrderDetailID",
                        column: x => x.OrderDetailID,
                        principalTable: "OrderDetails",
                        principalColumn: "OrderDetailID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_Products_CategoryID",
                table: "Category_Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Products_ProductID",
                table: "Category_Products",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Color_ProductDetail_ColorID",
                table: "Color_ProductDetail",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_Color_ProductDetail_ProductDetailID",
                table: "Color_ProductDetail",
                column: "ProductDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSupportMessages_CustomerSupportTicketTicketID",
                table: "CustomerSupportMessages",
                column: "CustomerSupportTicketTicketID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSupportTickets_UserID",
                table: "CustomerSupportTickets",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ProductDetail_LogID",
                table: "Inventory_ProductDetail",
                column: "LogID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ProductDetail_ProductDetailsID",
                table: "Inventory_ProductDetail",
                column: "ProductDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLogs_ColorID",
                table: "InventoryLogs",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLogs_SizeID",
                table: "InventoryLogs",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ColorID",
                table: "OrderDetails",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductID",
                table: "OrderDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ShippingMethodID",
                table: "OrderDetails",
                column: "ShippingMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_SizeID",
                table: "OrderDetails",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_VoucherID",
                table: "OrderDetails",
                column: "VoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodID",
                table: "Orders",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTrackings_OrderDetailID",
                table: "OrderTrackings",
                column: "OrderDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodDetails_PaymentMethodID",
                table: "PaymentMethodDetails",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Details_Sizes_ProductDetailID",
                table: "Product_Details_Sizes",
                column: "ProductDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Details_Sizes_SizeID",
                table: "Product_Details_Sizes",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ImageID",
                table: "ProductDetails",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductID",
                table: "ProductDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarranties_ProductID",
                table: "ProductWarranties",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ProductID",
                table: "Ratings",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserID",
                table: "Ratings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Returns_OrderID",
                table: "Returns",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserID",
                table: "Roles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductID",
                table: "Sales",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ColorID",
                table: "ShoppingCarts",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_SizeID",
                table: "ShoppingCarts",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserID",
                table: "ShoppingCarts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_VoucherID",
                table: "ShoppingCarts",
                column: "VoucherID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_RoleID",
                table: "User_Roles",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_UserID",
                table: "User_Roles",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ImageID",
                table: "Users",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_CategoryID",
                table: "Vouchers",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Vouchers_ProductID",
                table: "Vouchers",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category_Products");

            migrationBuilder.DropTable(
                name: "Color_ProductDetail");

            migrationBuilder.DropTable(
                name: "CustomerSupportMessages");

            migrationBuilder.DropTable(
                name: "Inventory_ProductDetail");

            migrationBuilder.DropTable(
                name: "OrderTrackings");

            migrationBuilder.DropTable(
                name: "PaymentMethodDetails");

            migrationBuilder.DropTable(
                name: "Product_Details_Sizes");

            migrationBuilder.DropTable(
                name: "ProductWarranties");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Returns");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "User_Roles");

            migrationBuilder.DropTable(
                name: "CustomerSupportTickets");

            migrationBuilder.DropTable(
                name: "InventoryLogs");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ShippingMethods");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
