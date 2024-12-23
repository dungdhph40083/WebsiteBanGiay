using Application.Data.ModelContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient("DefaultClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7187/swagger/index.html");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

// Cấu hình DbContext
builder.Services.AddDbContext<GiayDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseBanGiay")));

// Thêm dịch vụ vào container
builder.Services.AddControllersWithViews();

builder.Services.AddMvc().AddMvcOptions(o => o.AllowEmptyInputInBodyModelBinding = true);

// Cấu hình Session
builder.Services.AddSession(Options =>
{
    Options.IdleTimeout = TimeSpan.FromMinutes(14);
    Options.Cookie.HttpOnly = true;
    Options.Cookie.IsEssential = true;
});

// Đăng ký HttpClient
builder.Services.AddHttpClient();

// Đăng ký HttpContextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build(); // Dòng này đã đủ, không cần khai báo lại!

// Cấu hình Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();
app.UseAuthorization();

// Cấu hình Endpoint
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
