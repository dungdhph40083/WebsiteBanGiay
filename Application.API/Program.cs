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
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentMethod, PaymentMethodRepository>();
builder.Services.AddScoped<IPaymentMethodDetail, PaymentMethodDetailRepository>();
builder.Services.AddScoped<IOrderTracking, OrderTrackingRepository>();
builder.Services.AddScoped<IOrderDetails, OrderDetailsRepository>();
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddScoped<IShoppingCart, ShoppingCartRepository>();
builder.Services.AddScoped<ISize, SizeRepository>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<IVoucher, VoucherRepository>();
builder.Services.AddScoped<IProductDetail, ProductDetailRepository>();
builder.Services.AddScoped<IProductWarranty, ProductWarrantyRepository>();
builder.Services.AddScoped<IRatings, RatingsRepository>();
builder.Services.AddScoped<IReturn, ReturnRepository>();
builder.Services.AddScoped<ISale, SaleRepository>();
builder.Services.AddScoped<IRole, RoleRepository>();
builder.Services.AddScoped<IShippingMethod, ShippingMethodRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Origin",
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                      });
});

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

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("Origin");

app.UseAuthorization();

app.MapControllers();

app.Run();
