using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repositories;

namespace StoreApp.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
        // Uygulama başlatıldığında veritabanı göçlerini kontrol eder ve uygular
        public static void ConfigureAndCheckMigrations(this IApplicationBuilder app)
        {
            // RepositoryContext oluşturmak için servis sağlayıcısından hizmet alır
            RepositoryContext context = app
                .ApplicationServices
                .CreateAsyncScope() // Yeni bir asenkron kapsam oluşturur
                .ServiceProvider // Servis sağlayıcısını alır
                .GetRequiredService<RepositoryContext>(); // RepositoryContext hizmetini alır

            // Bekleyen veritabanı göçleri olup olmadığını kontrol eder
            if (context.Database.GetPendingMigrations().Any())
            {
                // Bekleyen göçler varsa, bunları veritabanına uygular
                context.Database.Migrate();
            }
        }

        // Uygulamanın yerelleştirme ayarlarını yapılandırır
        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(options =>
            {
                // Desteklenen kültürleri ekler ve varsayılan kültürü ayarlar
                options.AddSupportedCultures("tr-TR") // Desteklenen kültürler listesine Türkçe'yi ekler
                .AddSupportedUICultures("tr-TR") // Desteklenen UI kültürler listesine Türkçe'yi ekler
                .SetDefaultCulture("tr-TR"); // Varsayılan kültürü Türkçe olarak ayarlar
            });
        }

        // Uygulama başlatıldığında varsayılan bir admin kullanıcısı oluşturur
        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
        {
            const string adminUser = "Admin"; // Varsayılan admin kullanıcı adı
            const string adminPassword = "Admin+123456"; // Varsayılan admin kullanıcı şifresi

            // UserManager servisini oluşturur
            UserManager<IdentityUser> userManager = app
                .ApplicationServices
                .CreateScope() // Yeni bir kapsam oluşturur
                .ServiceProvider // Servis sağlayıcısını alır
                .GetRequiredService<UserManager<IdentityUser>>(); // UserManager hizmetini alır

            // RoleManager servisini oluşturur
            RoleManager<IdentityRole> roleManager = app
                    .ApplicationServices
                    .CreateScope() // Yeni bir kapsam oluşturur
                    .ServiceProvider // Servis sağlayıcısını alır
                    .GetRequiredService<RoleManager<IdentityRole>>(); // RoleManager hizmetini alır

            // Admin kullanıcısının mevcut olup olmadığını kontrol eder
            IdentityUser user = await userManager.FindByNameAsync(adminUser);

            if (user == null)
            {
                // Admin kullanıcısı mevcut değilse, yeni bir admin kullanıcısı oluşturur
                user = new IdentityUser(adminUser)
                {
                    Email = "tugba.buyuk@std.yildiz.edu.tr", // Admin kullanıcısının e-posta adresi
                    PhoneNumber = "05344065963", // Admin kullanıcısının telefon numarası
                    UserName = adminUser, // Admin kullanıcısının kullanıcı adı
                    EmailConfirmed = true // E-posta doğrulama durumunu belirtir
                };

                // Yeni admin kullanıcısını oluşturur
                var result = await userManager.CreateAsync(user, adminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception("Admin user could not created."); // Kullanıcı oluşturma başarısız olursa hata fırlatır
                }

                // Admin kullanıcısını tüm rollere atar
                var roleResult = await userManager.AddToRolesAsync(user,
                    roleManager
                        .Roles // Mevcut rolleri alır
                        .Select(r => r.Name) // Rollerden isimleri seçer
                        .ToList() // İsimleri listeye dönüştürür
                );

                if (!roleResult.Succeeded)
                    throw new Exception("System have problems with role defination for admin."); // Rol atama başarısız olursa hata fırlatır
            }
        }
    }
}


