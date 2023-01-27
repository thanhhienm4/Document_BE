using Document.Data.EF;
using Document.RestService.Repositories;
using Document.RestService.Repositories.Impl;
using Document.RestService.Services;
using Document.RestService.Services.Impl;
using Document.RestService.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var dbName = "DocumentDb";
var connectionString = builder.Configuration.GetConnectionString("DocumentDb") ?? throw new InvalidOperationException($"Connection string {dbName} not found.");

builder.Services.AddDbContext<DocumentContext>(options =>   
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DocumentContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITableRepository, TableRepository>();
//builder.Services.AddTransient<IDbContextTransaction, DbContextTransaction>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<ITableService, TableService>();

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
