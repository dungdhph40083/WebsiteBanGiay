using AutoMapper;
using Application.Data.DTOs;
using Application.Data.Models;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Application.Data.ModelContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient("DefaultClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7187/swagger/index.html");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

// Đăng ký AutoMapper
builder.Services.AddAutoMapper(typeof(Program)); 

// Đăng ký DbContext và các service khác
builder.Services.AddDbContext<GiayDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseBanGiay")).EnableSensitiveDataLogging());

// Đăng ký các service khác, bao gồm cả repository
builder.Services.AddScoped<ICustomerSupportMessage, CustomerSupportMessageRepository>();

// Add INotyfService
builder.Services.AddScoped<INotyfService, NotyfService>();
// Add Notyf // Đây là thông báo
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 3;
    config.IsDismissable = true;
    config.HasRippleEffect = true;
    config.Position = NotyfPosition.TopRight;
});



builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddMvc().AddMvcOptions(o => o.AllowEmptyInputInBodyModelBinding = true);

// Các dịch vụ khác
builder.Services.AddSession(Options =>
{
    Options.IdleTimeout = TimeSpan.FromMinutes(14);
    Options.Cookie.HttpOnly = true;
    Options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", options =>
    {
        options.LoginPath = "/Login/Login";
        options.LogoutPath = "/Profile/Logout";
    });
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Cấu hình các middleware
app.UseRouting();
app.UseStaticFiles();
app.UseSession();
app.UseNotyf();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
