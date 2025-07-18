using ASPCoreWebAPICRUD.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("dbcs");

// ✅ Register DbContext with SQL Server
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(connectionString));

// ✅ Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
