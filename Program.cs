using HastaneRandevuSistemi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("EFHastaneRandevuContextConnection") ?? throw new InvalidOperationException("Connection string 'EFHastaneRandevuContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();

var DbcConnection = "Server=(localdb)\\mssqllocaldb;Database=EFHastaneRandevu;Trusted_Connection=true;";

builder.Services.AddDbContext<EFHastaneRandevuContext>(options => options.UseSqlServer(DbcConnection));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<EFHastaneRandevuContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Kullanici}/{controller=Home}/{action=Index}/{id?}");

app.Run();