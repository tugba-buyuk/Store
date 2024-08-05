using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Extensions;
using Services;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;
using Stripe;
using Twilio;
using System.Net.Mail;
using Entities.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureSession();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureRepositoryRegistration();
builder.Services.ConfigureServiceRegistration();
builder.Services.ConfigureApplicationCookie();

builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));


builder.Services.ConfigureRouting();
builder.Services.AddFluentEmail(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.ConfigureAndCheckMigrations();
app.ConfigureLocalization();
app.ConfigureDefaultAdminUser();

app.Run();