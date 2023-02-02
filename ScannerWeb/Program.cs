using Microsoft.EntityFrameworkCore;
using ScannerWeb;
using ScannerWeb.Entities;
using ScannerWeb.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ScannerDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("db")));
builder.Services.AddScoped<ScannerDbContext>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IScannerService, ScannerService>();
var app = builder.Build();
var scope = app.Services.CreateScope();

	app.UseSwagger();
	app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
