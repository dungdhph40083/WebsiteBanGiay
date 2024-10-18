using Application.Data.ModelContexts;
using Application.Data;
using Microsoft.EntityFrameworkCore;
using Application.Data.Repositories.IRepository;
using Application.Data.Repositories;
using AutoMapper;
using Application.Data.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IShoppingCart, ShoppingCartRepository>();


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
