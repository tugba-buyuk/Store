using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using Services;
using Entities.Models;
using StoreApp.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.Google;

namespace StoreApp.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
                {
                    options.UseSqlite(configuration.GetConnectionString("sqlconnection"), b => b.MigrationsAssembly("StoreApp"));
                    options.EnableSensitiveDataLogging(true);
                });

        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;

            }).AddDefaultUI()
                .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders(); ;
        }

        public static void ConfigureSession(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "StoreApp.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<Cart>(c => SessionCart.GetCart(c));
        }

        public static void ConfigureRepositoryRegistration(this IServiceCollection services)
        {

            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ICouponCodeRepository, CouponCodeRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();


        }

        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IColorService, ColorManager>();
            services.AddScoped<ICouponCodeService, CouponCodeManager>();
            services.AddScoped<ICityService, CityManager>();
            services.AddScoped<ICountryService, CountryManager>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddTransient<IEmailSender, EmailSender>();
        }
        public static void ConfigureRouting(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = false;
            });
        }

        public static void ConfigureApplicationCookie(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.AccessDeniedPath = new PathString("/Account/AccessDenied");
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection = configuration.GetSection("Authentication:Google");
                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
            });
        }
    }
}
