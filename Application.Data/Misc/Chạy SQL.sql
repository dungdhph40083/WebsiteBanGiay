create database DatabaseBanGiay
go

use DatabaseBanGiay
go

create table Images
(
ImageID					uniqueidentifier	not null	primary key,
ImageName				nvarchar(MAX),
ImageDescription		nvarchar(MAX),
ImageFileName			nvarchar(MAX),
[Status]				tinyint,
CreatedAt				datetime,
UpdatedAt				datetime
)
go

-- LƯU Ý: KIỂM TRA VÀ SỬA ĐƯỜNG DẪN TỚI TỆP TRƯỚC KHI DÙNG --
insert into Images values
('2ac06cd1-492f-47f2-bdc3-6e8ce031b3ab', N'Adidas 8152',				N'(Mô tả hình ảnh bằng chữ ở đây)', N'adidas-8152.jpg',					1, GetDate(), GetDate()),
('7fcf6f9a-7352-423a-8bf9-612d542b3522', N'Adidas A83',					N'(Mô tả hình ảnh bằng chữ ở đây)', N'adidas-a83.jpg',					1, GetDate(), GetDate()),
('05f20b0f-6979-4e82-a727-cb24f626d478', N'Adidas Đen-trắng Galaxy 5',	N'(Mô tả hình ảnh bằng chữ ở đây)', N'adidas-black-white-galaxy5.jpg',	1, GetDate(), GetDate()),
('79ba4596-0578-45ad-8109-a7d5029ab4d9', N'Adidas Da bò',				N'(Mô tả hình ảnh bằng chữ ở đây)', N'adidas-da-bo.jpg',				1, GetDate(), GetDate()),
('8d59af65-660c-4a93-a91c-754218be7de3', N'Adidas Trắng sọc đen',		N'(Mô tả hình ảnh bằng chữ ở đây)', N'adidas-trang-soc-den.jpg',		1, GetDate(), GetDate())
																		
/* region Category-Products */
create table Categories
(
CategoryID				uniqueidentifier	not null	primary key,
CategoryName			nvarchar(MAX),
[Description]			nvarchar(MAX)
)

insert into Categories values
('414f0af6-366f-4bd7-a09d-510193f533ea', N'Giày da', N'Dành cho các loại giày làm bằng chất liệu da thú'),
('0d3a5b93-0087-4d99-bddb-d5dface1fa95', N'Giày thể thao', N'Dành cho các loại giày dành cho vận động thể thao'),
('96dbcc88-1558-4bbb-a483-3804b24f086e', N'Giày thời trang', N'Dành cho các loại giày thời trang')

create table Products
(
ProductID				uniqueidentifier	not null	primary key,
[Name]					nvarchar(MAX),
[Description]			nvarchar(MAX),
Price					bigint,
CreatedAt				datetime,
UpdatedAt				datetime
)

insert into Products values
('6bfe3e5d-9d02-4745-9e05-41ef76a8d693', N'Giày Adidas 8152',				N'Adidas 8152 dành cho thể thao và thời trang, giúp giảm mỏi chân khi vận động nặng',	172000, GETDATE(), GETDATE()),
('a785be42-a9de-44f8-b826-7bc9151eea9b', N'Giày Adidas A83',				N'Adidas A83 dành cho thể thao, giúp giảm mỏi chân khi vận động nặng',					144000, GETDATE(), GETDATE()),
('87e469e0-45ed-4eda-b929-5d3dc3789bf1', N'Giày Adidas Đen-trắng Galaxy 5', N'Adidas Đen-trắng nam tính, thời trang hot nhất',										126000, GETDATE(), GETDATE()),
('61ba03de-fbd6-41dd-8ebf-53a400cfa0db', N'Giày Adidas Da bò',				N'Adidas Da bò hiếm có, đẹp mắt, hợp gu',												193000, GETDATE(), GETDATE()),
('4f13dc6f-9b7e-4f96-9a82-d0ea01e3a8a7', N'Giày Adidas Trắng sọc đen',		N'Adidas Trắng sọc đen dành cho thể thao, giúp giảm mỏi chân khi vận động nặng',		126000, GETDATE(), GETDATE())

create table Category_Products
(
Category_Products_ID	uniqueidentifier	not null	primary key,
ProductID				uniqueidentifier,
CategoryID				uniqueidentifier,

constraint FK_Categories_Categories_Products
foreign key (CategoryID) references Categories(CategoryID),

constraint FK_Products_Categories_Products
foreign key (ProductID) references Products(ProductID)
)

insert into Category_Products values
(NEWID(), '6bfe3e5d-9d02-4745-9e05-41ef76a8d693', '0d3a5b93-0087-4d99-bddb-d5dface1fa95'),
(NEWID(), '6bfe3e5d-9d02-4745-9e05-41ef76a8d693', '96dbcc88-1558-4bbb-a483-3804b24f086e'),
(NEWID(), 'a785be42-a9de-44f8-b826-7bc9151eea9b', '0d3a5b93-0087-4d99-bddb-d5dface1fa95'),
(NEWID(), '87e469e0-45ed-4eda-b929-5d3dc3789bf1', '96dbcc88-1558-4bbb-a483-3804b24f086e'),
(NEWID(), '61ba03de-fbd6-41dd-8ebf-53a400cfa0db', '414f0af6-366f-4bd7-a09d-510193f533ea'),
(NEWID(), '4f13dc6f-9b7e-4f96-9a82-d0ea01e3a8a7', '0d3a5b93-0087-4d99-bddb-d5dface1fa95')

/* region User-Roles */
create table Users
(
UserID					uniqueidentifier	not null	primary key,
Username				varchar(30)			not null	unique,
[Password]				nvarchar(MAX)		not null,
Email					varchar(MAX),
ImageID					uniqueidentifier,
[Address]				nvarchar(MAX),
PhoneNumber				varchar(30),
CreatedAt				datetime,
LastUpdatedOn			datetime,
[Status]				tinyint,

constraint FK_Images_Users
foreign key (ImageID) references Images(ImageID)
)

insert into Users values
('2aab6663-80a4-44ce-8139-c679be51318c', 'admin', '$2a$14$zKVa.FOju5djwNqTg5jckeibUpOuOEJ4SyQXtGNH7ovseME2ubqpK', 'a@a.com', null, N'Hà Nội', '0123456789', GETDATE(), GETDATE(), 1),
('911729e5-b813-453f-9c6b-11dfae751464', 'user',  '$2a$14$mNYjhrkDnEgd12KlXSHfxu7/4HdDVvgVUagEEv8M0XXkihvAlKndK', 'a@b.com', null, N'Hà Nội', '0123456789', GETDATE(), GETDATE(), 1)

create table Roles
(
RoleID					uniqueidentifier	not null	primary key,
RoleCode				varchar(100)		not null	unique,
RoleName				nvarchar(MAX)
)

insert into Roles values
('9ba7bf79-19c1-4611-b44b-69fc8023842e', 'ADMINISTRATOR', N'Quản trị viên'),
('1bfa7246-60e1-4d82-a469-cdecf867fd01', 'USER', N'Người dùng'),
('b463986e-60f1-4029-91c4-1116c4e073e7', 'BANNED', N'Bị cấm')

create table User_Roles
(
User_RoleID				uniqueidentifier	not null	primary key,
UserID					uniqueidentifier,
RoleID					uniqueidentifier,

constraint FK_Users_User_Roles
foreign key (UserID) references Users(UserID),

constraint FK_Roles_User_Roles
foreign key (RoleID) references Roles(RoleID)
)

insert into User_Roles values
(NEWID(), '2aab6663-80a4-44ce-8139-c679be51318c', '9ba7bf79-19c1-4611-b44b-69fc8023842e'),
(NEWID(), '2aab6663-80a4-44ce-8139-c679be51318c', '1bfa7246-60e1-4d82-a469-cdecf867fd01'),
(NEWID(), '911729e5-b813-453f-9c6b-11dfae751464', '1bfa7246-60e1-4d82-a469-cdecf867fd01')

create table Sales
(
SaleID					uniqueidentifier	not null	primary key,
[Name]					nvarchar(MAX),
SaleCode				nvarchar(MAX),
[Status]				tinyint,
CreatedAt				datetime,
UpdatedAt				datetime,
ProductID				uniqueidentifier

constraint FK_Sales_Products
foreign key (ProductID) references Products(ProductID)
)

insert into Sales values
(NEWID(), N'Giảm giá khai trương', N'GRANDOPENING25', 1, GETDATE(), GETDATE(), '6bfe3e5d-9d02-4745-9e05-41ef76a8d693')

create table ProductWarranties
(
WarrantyID				uniqueidentifier	not null	primary key,
WarrantyPeriod			datetime,
WarrantyConditions		nvarchar(MAX),
CreatedAt				datetime,
UpdatedAt				datetime,
ProductID				uniqueidentifier

constraint FK_ProductWarranties_Products
foreign key (ProductID) references Products(ProductID)
)

insert into ProductWarranties values
(NEWID(), GETDATE(), N'Nhãn hàng còn nguyên vẹn', GETDATE(), GETDATE(), '6bfe3e5d-9d02-4745-9e05-41ef76a8d693'),
(NEWID(), GETDATE(), N'Nhãn hàng còn nguyên vẹn', GETDATE(), GETDATE(), 'a785be42-a9de-44f8-b826-7bc9151eea9b'),
(NEWID(), GETDATE(), N'Nhãn hàng còn nguyên vẹn', GETDATE(), GETDATE(), '87e469e0-45ed-4eda-b929-5d3dc3789bf1'),
(NEWID(), GETDATE(), N'Nhãn hàng còn nguyên vẹn', GETDATE(), GETDATE(), '61ba03de-fbd6-41dd-8ebf-53a400cfa0db'),
(NEWID(), GETDATE(), N'Nhãn hàng còn nguyên vẹn', GETDATE(), GETDATE(), '4f13dc6f-9b7e-4f96-9a82-d0ea01e3a8a7')

create table ShippingMethods
(
ShippingMethodID		uniqueidentifier	not null	primary key,
MethodName				nvarchar(MAX),
ShippingFee				bigint,
EstimatedDeliveryTime	datetime
)

create table PaymentMethods
(
PaymentMethodID			uniqueidentifier	not null	primary key,
MethodName				nvarchar(MAX),
[Status]				tinyint,
[Description]			nvarchar(MAX),
CreatedAt				datetime,
UpdatedAt				datetime
)

insert into PaymentMethods values
(NEWID(), N'Tiền mặt', 1, N'Khách thanh toán bằng tiền mặt trực tiếp', GETDATE(), GETDATE()),
(NEWID(), N'Chuyển khoản', 1, N'Khách thanh toán bằng chuyển khoản', GETDATE(), GETDATE())

create table PaymentMethodDetails
(
PaymentMethodDetails	uniqueidentifier	not null	primary key,
PaymentMethodID			uniqueidentifier,
TotalMoney				bigint,
[Status]				tinyint,
[Description]			nvarchar(MAX),

constraint FK_PaymentMethods_PaymentMethodDetails
foreign key (PaymentMethodID) references PaymentMethods(PaymentMethodID)
)

create table Ratings
(
RatingID				uniqueidentifier	not null	primary key,
UserID					uniqueidentifier,
ProductID				uniqueidentifier,
Stars					decimal(2, 1),
Comment					nvarchar(MAX),
DateRated				datetime,

constraint FK_Rating_User
foreign key (UserID) references Users(UserID),

constraint FK_Product_User
foreign key (ProductID) references Products(ProductID)
)

create table Sizes
(
SizeID					uniqueidentifier	not null	primary key,
[Name]					nvarchar(MAX),
[Status]				tinyint,
CreatedAt				datetime,
UpdatedAt				datetime
)

insert into Sizes values
('45eeaf74-7ec5-458c-9fca-50f57d716044', N'Nhỏ (S)', 1, GETDATE(), GETDATE()),
('9c4dcec5-64d0-4600-9363-d2478dad6823', N'Trung bình (M)', 1, GETDATE(), GETDATE()),
('88580723-ab1d-4362-93cd-f1ec0c8206fd', N'Lớn (L)', 1, GETDATE(), GETDATE()),
('ad6b200b-a8cb-40a7-b9de-14d65c2e235c', N'Siêu lớn (XL)', 1, GETDATE(), GETDATE()),
('055d6634-3a93-4016-a683-3ace85a10324', N'Siêu siêu lớn (XXL)', 1, GETDATE(), GETDATE())

create table Colors
(
ColorID					uniqueidentifier	not null	primary key,
ColorName				nvarchar(MAX),
[Status]				tinyint,
CreatedAt				datetime,
UpdatedAt				datetime
)

insert into Colors values
('f4e33645-1118-4662-99e2-ff5eeb18bc2c', N'Màu trắng', 1, GETDATE(), GETDATE()),
('c384f510-5e32-4ee7-92f2-e70e15f3623b', N'Màu đỏ', 1, GETDATE(), GETDATE()),
('424eb9f1-fdd4-4608-a9ce-e8d985466a48', N'Màu vàng', 1, GETDATE(), GETDATE()),
('0101956c-e127-4be6-8322-f9c46f49594b', N'Màu đen', 1, GETDATE(), GETDATE()),
('0809e816-083d-4b4b-b600-558472f2dfe2', N'Màu tím', 1, GETDATE(), GETDATE()),
('22a89021-c920-4b5b-9982-583acdee3e9d', N'Màu nâu', 1, GETDATE(), GETDATE()),
('f31e465c-dcc5-42b7-bbd9-7984c89c7159', N'Màu xanh dương', 1, GETDATE(), GETDATE()),
('9cc6436e-8e24-4065-9925-eccd56d6d099', N'Màu xanh lá', 1, GETDATE(), GETDATE()),
('8bb6b87b-4d97-490d-91f2-c57b2d40d820', N'Màu hồng', 1, GETDATE(), GETDATE())

create table ProductDetails
(
ProductDetailID			uniqueidentifier	not null	primary key,
ProductID				uniqueidentifier,
Material				nvarchar(MAX),
Quantity				int,
Price					bigint,
Brand					nvarchar(MAX),
PlaceOfOrigin			nvarchar(MAX),
[Type]					nvarchar(MAX),
WarrantyPeriod			datetime,
Instructions			nvarchar(MAX),
Features				nvarchar(MAX),
ImageID					uniqueidentifier,

constraint FK_Products_ProductDetails
foreign key (ProductID) references Products(ProductID),

constraint FK_Images_ProductDetails
foreign key (ImageID) references Images(ImageID)
)

insert into ProductDetails values
('ec093371-289f-4f78-a90a-f95a6cb5edec', '6bfe3e5d-9d02-4745-9e05-41ef76a8d693', N'Mô tả', 69420, 172000, N'Adidas', N'Nước ngoài', N'Giày thể thao & thời trang', GETDATE(), N'Không được sử dụng nước tẩy hoặc giặt ở nhiệt độ quá 65 độ C', N'Thể thao & thời trang', null),
('7e7c19ee-161b-4c04-82e9-56ca7676613f', 'a785be42-a9de-44f8-b826-7bc9151eea9b', N'Mô tả', 69420, 144000, N'Adidas', N'Nước ngoài', N'Giày thể thao & thời trang', GETDATE(), N'Không được sử dụng nước tẩy hoặc giặt ở nhiệt độ quá 65 độ C', N'Thể thao & thời trang', null),
('b6ac32d4-d03f-4ce0-9b19-31f41cd77438', '87e469e0-45ed-4eda-b929-5d3dc3789bf1', N'Mô tả', 69420, 126000, N'Adidas', N'Nước ngoài', N'Giày thể thao & thời trang', GETDATE(), N'Không được sử dụng nước tẩy hoặc giặt ở nhiệt độ quá 65 độ C', N'Thể thao & thời trang', null),
('c54d8c98-6884-4d21-9100-7ecafce37a56', '61ba03de-fbd6-41dd-8ebf-53a400cfa0db', N'Mô tả', 69420, 193000, N'Adidas', N'Nước ngoài', N'Giày thể thao & thời trang', GETDATE(), N'Không được sử dụng nước tẩy hoặc giặt ở nhiệt độ quá 65 độ C', N'Thể thao & thời trang', null),
('a0ad4a4a-8de5-430d-b5a5-34d0389e5a5c', '4f13dc6f-9b7e-4f96-9a82-d0ea01e3a8a7', N'Mô tả', 69420, 126000, N'Adidas', N'Nước ngoài', N'Giày thể thao & thời trang', GETDATE(), N'Không được sử dụng nước tẩy hoặc giặt ở nhiệt độ quá 65 độ C', N'Thể thao & thời trang', null)


create table InventoryLogs
(
LogID					uniqueidentifier	not null	primary key,
SizeID					uniqueidentifier,
ColorID					uniqueidentifier,
ProductDetailID			uniqueidentifier,
QuantityInStock			int,
[Status]				tinyint,
UpdatedAt				datetime,

constraint FK_Size_InventoryLogs
foreign key (SizeID) references Sizes(SizeID),

constraint FK_Color_InventoryLogs
foreign key (ColorID) references Colors(ColorID),

constraint FK_ProductDetail_InventoryLogs
foreign key (ProductDetailID) references ProductDetails(ProductDetailID)
)

insert into InventoryLogs values
('89837276-d9d8-4847-980e-eb7a93711673', '88580723-ab1d-4362-93cd-f1ec0c8206fd', N'0101956c-e127-4be6-8322-f9c46f49594b', 'ec093371-289f-4f78-a90a-f95a6cb5edec', 69420, 1, GETDATE()),
('d4b2b1da-1a36-49ad-80f2-9480da370f5e', '88580723-ab1d-4362-93cd-f1ec0c8206fd', N'0101956c-e127-4be6-8322-f9c46f49594b', '7e7c19ee-161b-4c04-82e9-56ca7676613f', 69420, 1, GETDATE()),
('3c0ab7ed-a5bc-4723-bb6b-6698a4c31d74', '88580723-ab1d-4362-93cd-f1ec0c8206fd', N'0101956c-e127-4be6-8322-f9c46f49594b', 'b6ac32d4-d03f-4ce0-9b19-31f41cd77438', 69420, 1, GETDATE()),
('8e170bac-b30b-4226-8339-e7fd91532199', '88580723-ab1d-4362-93cd-f1ec0c8206fd', N'0101956c-e127-4be6-8322-f9c46f49594b', 'c54d8c98-6884-4d21-9100-7ecafce37a56', 69420, 1, GETDATE()),
('09b0f4fd-33b9-45df-919b-75cfb3be32ad', '88580723-ab1d-4362-93cd-f1ec0c8206fd', N'0101956c-e127-4be6-8322-f9c46f49594b', 'a0ad4a4a-8de5-430d-b5a5-34d0389e5a5c', 69420, 1, GETDATE())

create table Size_ProductDetails
(
Size_ProductDetail_ID	uniqueidentifier	not null	primary key,
ProductID				uniqueidentifier,
SizeID					uniqueidentifier

constraint FK_Product_Size_ProductDetails
foreign key (ProductID) references Products(ProductID),

constraint FK_Size_Size_ProductDetails
foreign key (SizeID) references Sizes(SizeID)
)

insert into Size_ProductDetails values
(NEWID(), '6bfe3e5d-9d02-4745-9e05-41ef76a8d693', N'88580723-ab1d-4362-93cd-f1ec0c8206fd'),
(NEWID(), 'a785be42-a9de-44f8-b826-7bc9151eea9b', N'88580723-ab1d-4362-93cd-f1ec0c8206fd'),
(NEWID(), '87e469e0-45ed-4eda-b929-5d3dc3789bf1', N'88580723-ab1d-4362-93cd-f1ec0c8206fd'),
(NEWID(), '61ba03de-fbd6-41dd-8ebf-53a400cfa0db', N'88580723-ab1d-4362-93cd-f1ec0c8206fd'),
(NEWID(), '4f13dc6f-9b7e-4f96-9a82-d0ea01e3a8a7', N'88580723-ab1d-4362-93cd-f1ec0c8206fd')

create table Inventory_ProductDetails
(
Inventory_ProductDetailID	uniqueidentifier	not null	primary key,
LogID					uniqueidentifier,
ProductDetailID			uniqueidentifier

constraint FK_Inventory_Inventory_ProductDetails
foreign key (LogID) references InventoryLogs(LogID),

constraint FK_ProductDetail_Inventory_ProductDetails
foreign key (ProductDetailID) references ProductDetails(ProductDetailID)
)

insert into Inventory_ProductDetails values
(NEWID(), '89837276-d9d8-4847-980e-eb7a93711673', 'ec093371-289f-4f78-a90a-f95a6cb5edec'),
(NEWID(), 'd4b2b1da-1a36-49ad-80f2-9480da370f5e', '7e7c19ee-161b-4c04-82e9-56ca7676613f'),
(NEWID(), '3c0ab7ed-a5bc-4723-bb6b-6698a4c31d74', 'b6ac32d4-d03f-4ce0-9b19-31f41cd77438'),
(NEWID(), '8e170bac-b30b-4226-8339-e7fd91532199', 'c54d8c98-6884-4d21-9100-7ecafce37a56'),
(NEWID(), '09b0f4fd-33b9-45df-919b-75cfb3be32ad', 'a0ad4a4a-8de5-430d-b5a5-34d0389e5a5c')

create table Color_ProductDetails
(
Color_ProductDetailID	uniqueidentifier	not null	primary key,
ColorID					uniqueidentifier,
ProductDetailID			uniqueidentifier,

constraint FK_Color_Color_ProductDetails
foreign key (ColorID) references Colors(ColorID),

constraint FK_ProductDetail_Color_ProductDetails
foreign key (ProductDetailID) references ProductDetails(ProductDetailID)
)

insert into Color_ProductDetails values
(NEWID(), '0101956c-e127-4be6-8322-f9c46f49594b', 'ec093371-289f-4f78-a90a-f95a6cb5edec'),
(NEWID(), '0101956c-e127-4be6-8322-f9c46f49594b', '7e7c19ee-161b-4c04-82e9-56ca7676613f'),
(NEWID(), '0101956c-e127-4be6-8322-f9c46f49594b', 'b6ac32d4-d03f-4ce0-9b19-31f41cd77438'),
(NEWID(), '0101956c-e127-4be6-8322-f9c46f49594b', 'c54d8c98-6884-4d21-9100-7ecafce37a56'),
(NEWID(), '0101956c-e127-4be6-8322-f9c46f49594b', 'a0ad4a4a-8de5-430d-b5a5-34d0389e5a5c')

create table Vouchers
(
VoucherID				uniqueidentifier	not null	primary key,
CategoryID				uniqueidentifier,
ProductID				uniqueidentifier,
UsesLeft				int,
DiscountPrice			bigint,
DiscountPercent			decimal(5, 2),
[Description]			nvarchar(MAX),
CreatedAt				datetime,
LastUpdatedOn			datetime,
[Status]				tinyint,

constraint FK_Category_Vouchers
foreign key (CategoryID) references Categories(CategoryID),

constraint FK_Product_Vouchers
foreign key (ProductID) references Products(ProductID)
)

insert into Vouchers values
('152847ec-dc80-470a-88c4-1a555ed00632', '0d3a5b93-0087-4d99-bddb-d5dface1fa95', null, 22, 24000, 10, N'Giảm giá khai trương cho mặt hàng thuộc danh mục', GETDATE(), GETDATE(), 1),
('2e1b811e-c9fc-465f-85db-33d79525535b', '96dbcc88-1558-4bbb-a483-3804b24f086e', null, 22, 24000, 10, N'Giảm giá khai trương cho mặt hàng thuộc danh mục', GETDATE(), GETDATE(), 1),
('288a4e39-8a4b-4d20-9b7f-aedcfacbcc09', null, '6bfe3e5d-9d02-4745-9e05-41ef76a8d693', 44, 36000, 15, N'Giảm giá khai trương cho mặt hàng này', GETDATE(), GETDATE(), 1),
('04287033-79a1-4ccf-a09f-211efd446da9', null, 'a785be42-a9de-44f8-b826-7bc9151eea9b', 44, 36000, 15, N'Giảm giá khai trương cho mặt hàng này', GETDATE(), GETDATE(), 1),
('564ff9b2-8f4c-4e9b-8c83-eae212779591', null, '87e469e0-45ed-4eda-b929-5d3dc3789bf1', 44, 36000, 15, N'Giảm giá khai trương cho mặt hàng này', GETDATE(), GETDATE(), 1)

create table Orders
(
OrderID					uniqueidentifier	not null	primary key,
UserID					uniqueidentifier,
OrderDate				datetime,
[Status]				tinyint,
ShippingAddress			nvarchar(MAX),
PaymentMethodID			uniqueidentifier,

constraint FK_User_Orders
foreign key (UserID) references Users(UserID),

constraint FK_PaymentMethod_Orders
foreign key (PaymentMethodID) references PaymentMethods(PaymentMethodID)
)

create table OrderDetails
(
OrderDetailID			uniqueidentifier	not null	primary key,
OrderID					uniqueidentifier,
ProductID				uniqueidentifier,
Quantity				int,
Price					bigint,
TotalUnitPrice			bigint,
SizeID					uniqueidentifier,
ColorID					uniqueidentifier,
VoucherID				uniqueidentifier,
Discount				int,
SumTotalPrice			bigint,
CreatedAt				datetime,
ShippingMethodID		uniqueidentifier

constraint FK_Order_OrderDetails
foreign key (OrderID) references Orders(OrderID),

constraint FK_Product_OrderDetails
foreign key (ProductID) references Products(ProductID),

constraint FK_Size_Orders
foreign key (SizeID) references Sizes(SizeID),

constraint FK_Color_Orders
foreign key (ColorID) references Colors(ColorID),

constraint FK_Vouchers_OrderDetails
foreign key (VoucherID) references Vouchers(VoucherID),

constraint FK_ShippingMethod_OrderDetails
foreign key (ShippingMethodID) references ShippingMethods(ShippingMethodID)
)

create table OrderTrackings
(
TrackingID				uniqueidentifier	not null	primary key,
OrderDetailID			uniqueidentifier,
ShippingStatus			tinyint,
UpdatedAt				datetime

constraint FK_OrderDetail_OrderTrackings
foreign key (OrderDetailID) references OrderDetails(OrderDetailID)
)

create table ShoppingCarts
(
CartID					uniqueidentifier	not null	primary key,
UserID					uniqueidentifier,
ProductID				uniqueidentifier,
QuantityCart			int,
SizeID					uniqueidentifier,
ColorID					uniqueidentifier,
Price					bigint,
Discount				bigint,
DateAdded				datetime,
IsCheckedOut			bit,
VoucherID				uniqueidentifier

constraint FK_User_ShoppingCarts
foreign key (UserID) references Users(UserID),

constraint FK_Product_ShoppingCarts
foreign key (ProductID) references Products(ProductID),

constraint FK_Size_ShoppingCarts
foreign key (SizeID) references Sizes(SizeID),

constraint FK_Color_ShoppingCarts
foreign key (ColorID) references Colors(ColorID),

constraint FK_Voucher_ShoppingCarts
foreign key (VoucherID) references Vouchers(VoucherID)
)

create table [Returns]
(
ReturnID				uniqueidentifier	not null	primary key,
OrderID					uniqueidentifier,
Reason					nvarchar(MAX),
ReturnDate				datetime,
RefundAmount			bigint,
[Status]				tinyint,

constraint FK_Orders_Returns
foreign key (OrderID) references Orders(OrderID)
)

create table CustomerSupportTickets
(
TicketID				uniqueidentifier	not null	primary key,
UserID					uniqueidentifier,
[Subject]				nvarchar(MAX),
[Status]				tinyint,
CreatedAt				datetime

constraint FK_User_CustomerSupportTickets
foreign key (UserID) references Users(UserID)
)

create table CustomerSupportMessages
(
MessageID				bigint				identity(1, 1) primary key,
MessageContent			nvarchar(MAX),
TicketID				uniqueidentifier,

constraint FK_CustomerSupportMessage_CustomerSupportTickets
foreign key (TicketID) references CustomerSupportTickets(TicketID)
)

/*

⠀⠀⡠⣴⣿⢿⡟⠁⣠⢋⢜⠼⠁⣰⢟⣸⣿⣿⣿⣿⣿⠿⣱⣿⣿⠀⠀⠀⣀⣾⣿⣿⣿⣿⣿⠿⠟⠋⠁⠀⠀⠀⠀⠀⢀⠀⠀⠀⠀⠀⠀⠁⠀⠛⠿⠄⢠⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢠⠻⣧⠇⠘⡄⡚⡳⠓⢀⣾⡣⠀⡠⠂⡠⡒⣡⣿
⣠⡔⠀⣘⢿⠏⣌⡷⡡⣣⠞⢀⡴⣣⣤⣾⣿⣿⣿⡿⡋⠚⢿⣿⣿⠀⢀⣴⣿⣿⠿⠟⠋⠁⠀⠀⠀⠀⣀⠠⠔⠊⠉⠉⠀⠙⢦⣀⠀⠠⠀⠀⠀⠀⠀⠀⢿⣿⣆⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣀⣆⣽⢯⣉⢾⣖⡞⣾⣗⣉⣺⣿⠶⠁⢘⠎⢰⣿⣿
⠋⠀⣴⡷⠃⢠⢎⡵⣷⢃⣤⣿⣿⠟⠻⣿⣿⣿⠟⠀⠀⠀⢽⣿⡿⢦⠛⡟⠉⠀⠀⠀⠀⠀⠀⠀⡠⠊⠀⠀⠐⠂⠉⠉⠐⠠⡀⠻⣍⠓⢶⠦⠤⠠⠄⡀⠘⣿⣿⣿⡄⠀⠀⠀⠀⠠⠐⠂⠀⠀⠀⢺⣿⣿⡷⣮⣷⣿⣿⣿⣿⣿⢧⣤⣪⠃⢠⣿⣿⣿
⣤⣾⡟⡁⡰⢃⣾⢞⣵⣿⣿⡿⠋⠀⠀⠈⠙⠃⠀⠀⠀⠀⣼⣿⣷⣽⡍⠀⠀⠀⠀⠀⠀⠀⠀⠐⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢱⠀⠈⠳⠁⠀⠀⠀⠀⠈⠀⢹⣿⣿⣇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⣿⣿⣿⡿⠛⡇⢔⣻⣿⣶⣬⣿⡿⣟
⣿⣿⣾⣿⣷⣿⣿⣿⣿⣿⣿⣷⠀⠀⠂⠐⢶⣄⡀⠀⢀⣼⣿⡿⣿⡇⣿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⠿⣿⣿⣿⠟⠁⠁⣤⠙⡶⣅⢻⢉⣻⣿⣿⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣏⢿⡟⠛⠣⠀⠺⠷⢢⣤⡈⣠⣾⣿⣿⢋⣿⡇⣿⣷⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡠⠆⠀⠿⠿⢿⣆⠀⠀⠀⢰⠀⠀⠀⠀⠀⠀⠉⠀⣿⣿⡄⠀⣠⣴⠉⣸⡿⠨⢄⠌⠈⠂⢣⣾
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣤⣅⠀⠀⠀⠈⠉⢀⣼⣿⣿⠃⣸⣿⣇⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠠⠀⠒⠀⠀⠀⠀⠀⠀⠀⡀⠀⠀⢀⠁⠀⠁⠀⠀⠀⠀⠀⠀⣾⣧⠀⢠⠂⠀⢿⠏⠀⠀⠃⠀⣀⣰⣽⣿
⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⣄⣀⣠⣶⣿⣿⣿⠃⠀⢸⣿⡏⣿⣿⣿⠀⠀⠀⠀⣀⣀⠀⠖⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠡⢀⣴⣿⣿⣧⢰⡄⠀⠀⠀⡄⡇⠀⠀⢀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠛⣿⡷⠃⠀⡀⠀⢠⣀⠀⠀⡈⠟⠻⣿⣿
⠙⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡎⠀⠀⣿⣏⡇⣿⣿⣿⡀⠀⠀⠀⠀⠈⠀⠀⠀⠀⠀⠀⠀⠀⠠⠀⠀⠀⠀⠀⠀⣿⡿⣿⣿⡿⢻⠋⡄⢸⠀⣟⢿⠋⠠⠌⠀⠀⠀⠀⠀⠀⡄⠀⠀⣰⣿⣿⣦⡾⠀⣀⣼⠿⠗⠉⠀⠀⠀⢸⣿
⠀⠈⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⢻⣿⡇⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠀⡀⠂⠀⣀⣤⣤⣄⠀⠀⠹⠗⠛⠋⠀⠀⣒⣧⡼⢀⠋⠁⠈⠀⠀⠀⠀⠀⠀⡀⠀⠰⢶⣷⣿⣿⣿⡿⡷⠎⠋⠁⠀⠀⠀⡴⣠⣴⣿⣿
⠀⠀⠀⠀⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠀⠀⠀⢸⣿⢧⢿⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠀⠀⣼⣿⣿⣯⢻⠆⠐⠆⠀⢀⣤⡾⠀⣿⣿⠇⠈⢥⠀⠀⠀⠀⠀⡠⠐⣸⣣⠀⢀⢫⢾⣜⠽⠑⠉⠀⠀⢀⣀⠀⣠⣴⣿⣿⠿⣛⣛
⢤⣀⠀⠀⠀⠀⠨⡻⣿⣿⡿⣿⣿⣿⣿⣿⣿⣿⣿⣷⠀⠀⠀⢸⣿⣯⢯⣿⣿⡇⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠻⡟⠛⠁⠀⠀⢀⣴⣿⡿⠁⢠⡿⠋⢠⠀⢸⠀⠀⠀⠀⠀⠠⣀⣡⣋⣵⠁⠀⢫⠀⠀⠀⠀⠀⠀⡀⣿⣾⡿⢟⣫⣷⣿⣿⣿
⠀⠀⠁⠒⢄⠀⠀⠀⠍⠏⠀⠁⠙⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⣿⣿⣏⢯⣿⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠐⠂⠀⠀⠀⠀⠀⠀⠀⠀⠐⠛⠋⠉⠀⠀⠞⠁⠀⠀⡇⡸⠀⣤⣤⣶⣾⢿⡵⢝⠎⠁⠀⠀⠀⢢⢴⣆⣤⣶⣿⣿⣶⣶⣾⣿⣿⣿⣿⣿⣿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠂⢸⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⢸⣿⣿⠀⠙⢄⠀⠀⠀⠀⡄⡆⣀⠀⠀⠀⠀⡀⠀⠀⠀⠠⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢢⢃⢸⣿⣯⣝⡷⠉⠀⡐⠁⠀⠀⠀⢀⠀⠀⠑⣽⣟⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⠀⠀⠀⠀⠀⠀⠀⠀⡀⠀⠀⢀⠀⣾⣿⣿⣿⣿⣿⡿⣤⠀⠀⠀⠀⠛⠛⠀⠀⠀⠻⣦⣄⣀⠁⠈⠟⣰⣿⣆⠥⡈⠓⠒⠪⠥⣀⡀⠀⠀⠀⠀⠀⣰⡄⠀⠀⠀⠂⠈⠀⠳⠛⠁⠠⢶⠞⠒⠒⠷⡴⢾⡇⢦⠀⠀⠈⠻⣷⣝⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⠀⠀⠀⠀⠀⠀⠀⠀⠰⠀⠀⠌⣰⣿⣿⣻⣿⡿⣿⣶⣿⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠹⢿⣿⣿⣍⠉⠿⠀⡇⢼⠿⠦⠑⣢⢷⠈⢦⠀⠀⠀⠈⢞⢿⢿⠀⠀⠀⠀⠀⠀⠀⠀⠀⡠⠁⠀⠀⠀⠠⢹⡹⣷⣺⣦⡀⠀⠀⠘⢿⣿⣝⡿⣿⣿⣿⣿⣿⣿⣿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡔⣵⣿⣟⢺⣿⣽⣯⣿⣧⠜⣷⡀⠀⠀⠀⣠⠀⠀⠀⠀⠀⠀⠈⠉⠙⠀⢀⠀⡇⠀⣀⡀⢰⢻⢸⠀⠀⡄⠀⠀⠀⠀⠛⠈⠀⠀⠀⠀⠀⠀⠀⢀⠊⠀⢀⠀⠀⠀⠀⢠⢃⣺⣿⣿⣷⣄⠀⠀⠀⠙⢿⣿⣽⡻⣿⣿⣿⣿⣿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⣿⠟⠨⢽⡿⣿⣿⣿⣿⣿⣜⢷⣤⣤⣴⣿⠁⠀⠄⣶⣶⣶⣶⣾⠀⠀⠀⠀⡇⠀⠃⢷⠸⠀⢹⠀⠠⠁⠀⠀⠀⠀⠀⡄⠀⠀⠀⠀⠀⠐⡦⣧⡀⢠⠈⠀⠀⠀⠐⠒⠋⣷⡹⣿⣿⣿⣦⡀⠀⠀⠀⠙⢿⣿⣮⡻⣿⣿⣿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠁⠀⠀⣺⠇⠙⢻⣝⣿⡏⣫⢮⣻⣿⣿⣿⠀⠀⠀⠀⠈⠉⠉⠁⠀⠀⠀⠀⠘⣄⠀⠀⠀⠄⠈⡄⠁⢀⣠⣤⠀⠀⠀⠀⠀⠁⠀⠀⢀⠌⠀⠀⠉⠓⠞⡆⠀⠀⠀⠀⢀⣿⣿⡹⣿⣿⣿⣿⣦⠀⠀⠀⠀⠉⢻⣿⣮⣙⡿
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⡼⠀⠀⢠⡿⠻⡵⡛⣬⢿⣝⣻⠿⣀⠀⠀⠄⠀⡄⠀⠀⠀⢀⠖⠀⠀⠈⠛⢈⡈⣵⣷⣶⣿⣽⣿⠁⠀⠀⢀⠀⠀⡀⠀⡰⠁⠀⠀⠀⠀⠀⠀⣳⡦⣄⡀⠐⠛⣽⣿⣷⡜⢿⣿⣿⣿⣷⣄⠀⠀⠀⠀⠈⠫⣁⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠄⡀⣰⡟⢂⡾⠈⠀⠀⠀⠀⠀⠀⠀⠀⢀⠀⠀⠀⠀⣀⢀⣣⣤⣤⣤⣶⡟⠒⢴⣿⣿⣿⣿⢸⡇⠀⠀⠀⠸⠀⠀⣇⠜⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣙⣾⣿⢿⣿⣿⣿⣿⣞⢿⣿⣿⣿⠟⠃⠀⠀⠀⠠⠀⠀⠑
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⢚⡼⠋⣰⢫⢿⡀⠀⠀⢀⡠⣴⢦⣤⣤⣾⣿⣼⣿⣿⣿⣿⣿⣿⢸⣿⡟⠠⣠⣾⣿⣿⣿⣿⢸⡇⠀⠀⠀⢀⠀⡠⠁⠀⠀⠀⠀⠀⠀⠀⠀⡠⠔⠋⠀⠀⠀⠉⢻⣿⣟⣿⣿⡾⠋⠁⠀⠀⠀⠀⢀⠌⠀⠀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣼⢿⣁⡼⣷⠿⣺⠋⢈⡽⠁⣿⣻⣶⣿⣿⣿⡟⣿⣿⣿⣿⡿⠿⠋⠀⠈⣁⣄⣿⣿⣿⣿⣿⣿⢸⣷⣄⡀⠀⠈⠜⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣠⣶⣿⣿⠿⠋⠀⠀⠀⠀⠀⠀⡠⠂⠀⣀⡀⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⠈⡎⠈⢆⠞⠅⣠⠞⠉⠰⢺⠿⣿⣿⣿⣿⣧⣿⣿⣿⣿⣇⠀⠈⢆⢀⠘⣻⣿⣿⣿⣿⣿⣿⢸⣿⣿⣿⣷⣦⠂⠀⠄⠀⠀⠀⠀⠀⠀⠀⢀⠠⠄⠴⠞⠿⠿⠛⡑⠃⠀⠀⠀⠀⠀⠀⠀⢀⠔⠀⠂⢯⣞⡄⠀⠀
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣦⣿⡥⠿⠈⠑⠋⠤⢄⢳⣿⣮⣿⣿⣿⡝⠻⣿⣿⣿⣿⣿⣤⡀⠀⠘⢼⣿⣿⣿⣿⣿⣿⣿⣾⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠠⠀⠒⠈⠀⠀⠀⠀⠀⠀⢀⠐⠀⠀⠀⠀⠀⠀⠀⠀⠠⠂⠀⠀⠀⠀⠙⠠⠀⠀
⠐⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠹⣿⣿⡇⠀⠀⠀⠀⠀⠀⠉⢪⡻⣿⣿⡿⠁⠀⠘⣿⣿⣿⣿⣿⡇⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠐⠀⠀⣠⣤⡶⣼⡇⠀⢀⠔⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⢴⢧⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠿⢿⠀⠀⠀⠀⠀⠀⠀⠀⠙⣿⠉⠀⠀⠀⠀⢹⣿⣿⣿⣿⢱⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⢾⣽⡇⠠⠂⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⡀⠈⠳⠦⣸⠆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠉⠂⠄⠀⠀⠀⠈⠀⠀⠀⠀⠀⢸⣿⣿⣿⣿⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠈⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀

*/
