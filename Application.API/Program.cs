using Application.Data.ModelContexts;
using Application.Data;
using Microsoft.EntityFrameworkCore;
using Application.Data.Repositories.IRepository;
using Application.Data.Repositories;
using AutoMapper;
using Application.Data.Mappers;
using Application.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GiayDBContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseBanGiay"));
});

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryProductRepository, CategoryProductRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<ICustomerSupportTicketsRepository, CustomerSupportTicketsRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IInventoryLogRepository, InventoryLogRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentMethod, PaymentMethodRepository>();
builder.Services.AddScoped<IPaymentMethodDetail, PaymentMethodDetailRepository>();
builder.Services.AddScoped<IOrderTracking, OrderTrackingRepository>();
builder.Services.AddScoped<IOrderDetails, OrderDetailsRepository>();
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<IProductInventory, ProductInventoryRepository>();
builder.Services.AddScoped<IShoppingCart, ShoppingCartRepository>();
builder.Services.AddScoped<ISize_ProductDetail, Size_ProductDetailRepository>();
builder.Services.AddScoped<ISize, SizeRepository>();
builder.Services.AddScoped<IUser_Role, User_RoleRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IVoucher, VoucherRepository>();


var MapperConfig = new MapperConfiguration(Config =>
{
    Config.AddProfile(new MapProfile());
});
builder.Services.AddSingleton(MapperConfig.CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
