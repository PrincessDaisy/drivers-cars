using drivers_cars.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.Data;
using Repository.Interfaces;
using Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer("Server=DEV-PC004\\SQLEXPRESS;Database=test;Trusted_Connection=True;TrustServerCertificate=True;"));
builder.Services.AddTransient<ICarRepo, CarRepo>();
builder.Services.AddTransient<CarsService>();
builder.Services.AddTransient<IDriverRepo, DriverRepo>();
builder.Services.AddTransient<DriversService>();
builder.Services.AddTransient<IDriverCarMapRepo, DriverCarMapRepo>();
builder.Services.AddTransient<DriverCarMapsService>();


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
