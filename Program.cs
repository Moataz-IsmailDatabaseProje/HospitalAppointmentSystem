using HastaneRandevuSistemi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var DbcConnection = "Server=(localdb)\\mssqllocaldb;Database=EFHastaneRandevu;Trusted_Connection=true;";

builder.Services.AddDbContext<EFHastaneRandevuContext>(options => options.UseSqlServer(DbcConnection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
