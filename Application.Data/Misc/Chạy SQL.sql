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

create database DatabaseBanGiay
go

use DatabaseBanGiay
go

create table Images
(
ImageID					uniqueidentifier	not null	primary key,
ImageName				nvarchar(MAX),
ImageData				varbinary(MAX),
[Status]				tinyint,
CreatedAt				datetime,
UpdatedAt				datetime
)
go

begin /* region Category-Products */
create table Categories
(
CategoryID				uniqueidentifier	not null	primary key,
CategoryName			nvarchar(MAX),
[Description]			nvarchar(MAX)
)

create table Products
(
ProductID				uniqueidentifier	not null	primary key,
[Name]					nvarchar(MAX),
[Description]			nvarchar(MAX),
Price					bigint,
CreatedAt				datetime,
UpdatedAt				datetime
)

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
end

begin /* region User-Roles */
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

create table Roles
(
RoleID					uniqueidentifier	not null	primary key,
RoleName				nvarchar(MAX)
)

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
end

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
Stars					decimal(1, 1),
Comment					uniqueidentifier,
DateRated				datetime,

constraint FK_Rating_User
foreign key (UserID) references Users(UserID),

constraint FK_Product_User
foreign key (ProductID) references Products(ProductID)
)

begin /* region Sizes&Colors&Logs etc. */

create table Sizes
(
SizeID					uniqueidentifier	not null	primary key,
[Name]					nvarchar(MAX),
[Status]				tinyint,
CreatedAt				datetime,
UpdatedAt				datetime
)

create table Colors
(
ColorID					uniqueidentifier	not null	primary key,
ColorName				nvarchar(MAX),
[Status]				tinyint,
CreatedAt				datetime,
UpdatedAt				datetime
)

create table InventoryLogs
(
LogID					uniqueidentifier	not null	primary key,
SizeID					uniqueidentifier,
ColorID					uniqueidentifier,
QuantityInStock			int,
[Status]				tinyint,
UpdatedAt				datetime,

constraint FK_Size_InventoryLogs
foreign key (SizeID) references Sizes(SizeID),

constraint FK_Color_InventoryLogs
foreign key (ColorID) references Colors(ColorID)
)

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
end

create table Vouchers
(
VoucherID				uniqueidentifier	not null	primary key,
CategoryID				uniqueidentifier,
ProductID				uniqueidentifier,
UsesLeft				int,
DiscountPrice			bigint,
DiscountPercent			decimal(3,2),
[Description]			nvarchar(MAX),
CreatedAt				datetime,
LastUpdatedOn			datetime,
[Status]				tinyint,

constraint FK_Category_Vouchers
foreign key (CategoryID) references Categories(CategoryID),

constraint FK_Product_Vouchers
foreign key (ProductID) references Products(ProductID)
)

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