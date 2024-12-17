using Application.Data.ModelContexts;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient("DefaultClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7187/swagger/index.html");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddDbContext<GiayDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseBanGiay")));
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc().AddMvcOptions(o => o.AllowEmptyInputInBodyModelBinding = true);

builder.Services.AddSession(Options =>
{
    Options.IdleTimeout = TimeSpan.FromMinutes(14);
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
