using copyqr.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
IpConfiguration.Configurate();

string database = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "QrInfo";
string ip = Environment.GetEnvironmentVariable("DB_IP") ?? "localhost";
string username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
string password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "1337";

QrInfoContext.SetConnectionString($"Host={ip};" +
    $"Port=5432;" +
    $"Database={database};" +
    $"Username={username};" +
    $"Password={password}");


builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QrInfoContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
