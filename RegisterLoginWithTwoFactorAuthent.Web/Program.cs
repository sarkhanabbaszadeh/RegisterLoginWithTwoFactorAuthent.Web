using Microsoft.EntityFrameworkCore;
using RegisterLoginWithTwoFactorAuthent.Web.Extensions;
using RegisterLoginWithTwoFactorAuthent.Web.Models;
using RegisterLoginWithTwoFactorAuthent.Web.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr"));
});

builder.Services.AddIdentityWithExt();
builder.Services.AddScoped<TwoFactorService>();

builder.Services.ConfigureApplicationCookie(options =>
{
    var cookieBuilder = new CookieBuilder();

    cookieBuilder.Name = "RegLogAppCookie";
    options.LoginPath = new PathString("/Home/Signin");
    options.LogoutPath = new PathString("/Member/Logout");
    options.Cookie = cookieBuilder;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
