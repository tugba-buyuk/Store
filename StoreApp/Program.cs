using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Extensions;
using Services;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;
using Stripe;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureSession();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureRepositoryRegistration();
builder.Services.ConfigureServiceRegistration();
builder.Services.ConfigureApplicationCookie(options => { options.Cookie.SameSite = SameSiteMode.None; });

builder.Services.ConfigureRouting();
builder.Services.AddFluentEmail(builder.Configuration);
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseStaticFiles();
app.UseSession(); 
app.UseHttpsRedirection();

app.UseRouting();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();

    endpoints.MapControllers();
});


app.ConfigureAndCheckMigrations();
app.ConfigureLocalization();
app.ConfigureDefaultAdminUser();
app.Run();

//dotnet ef database update gerekiyor eve geçince bi bak.
//configuration hatasý olabilir debugla
