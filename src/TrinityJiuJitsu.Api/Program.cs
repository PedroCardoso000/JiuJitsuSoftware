using Microsoft.EntityFrameworkCore;
using TrinityJiuJitsu.Api.Extensions;
using TrinityJiuJitsu.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// SQLite - zero config, database file created automatically
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

// Dependency Injection
builder.Services.AddApplicationServices();

// Swagger + Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Trinity Jiu-Jitsu API", Version = "v1" });
});

var app = builder.Build();

// Auto-create database + apply migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
