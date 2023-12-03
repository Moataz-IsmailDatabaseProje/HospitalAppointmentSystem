using HastaneRandevuSistemi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("EFHastaneRandevuContextConnection") ?? throw new InvalidOperationException("Connection string 'EFHastaneRandevuContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();

var DbcConnection = "Server=(localdb)\\mssqllocaldb;Database=EFHastaneRandevu;Trusted_Connection=true;";

builder.Services.AddDbContext<EFHastaneRandevuContext>(options => options.UseSqlServer(DbcConnection));

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<EFHastaneRandevuContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSender>();

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