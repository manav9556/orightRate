using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using MySql.Data.MySqlClient;
using OrightApi.ExcelFile;
using System.Reflection;
using OfficeOpenXml;
using OrightApi.Repository;

var builder = WebApplication.CreateBuilder(args);
ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set license context to NonCommercial

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    
});


// Add the database connection service
builder.Services.AddTransient<IDbConnection>((sp) =>
    new MySqlConnection(builder.Configuration.GetConnectionString("conStr")));

// Register your repositories and services
builder.Services.AddTransient<SaveDataRepository,SaveDataRepository>();
builder.Services.AddTransient<ExcelConvert, ExcelConvert>(); 
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
