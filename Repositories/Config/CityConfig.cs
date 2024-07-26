using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.CityId);
            builder.Property(c => c.CityName).IsRequired();
            builder.HasData(
                new City() { CityId = 1, CityName = "Adana", CountryId=1 },
                new City() { CityId = 2, CityName = "Adıyaman", CountryId=1 },
                new City() { CityId = 3, CityName = "Afyonkarahisar", CountryId=1 },
                new City() { CityId = 4, CityName = "Ağrı", CountryId=1 },
                new City() { CityId = 5, CityName = "Amasya", CountryId=1 },
                new City() { CityId = 6, CityName = "Ankara", CountryId=1 },
                new City() { CityId = 7, CityName = "Antalya", CountryId=1 },
                new City() { CityId = 8, CityName = "Artvin", CountryId=1 },
                new City() { CityId = 9, CityName = "Aydın", CountryId=1 },
                new City() { CityId = 10, CityName = "Balıkesir", CountryId=1 },
                new City() { CityId = 11, CityName = "Bilecik", CountryId=1 },
                new City() { CityId = 12, CityName = "Bingöl", CountryId=1 },
                new City() { CityId = 13, CityName = "Bitlis", CountryId=1 },
                new City() { CityId = 14, CityName = "Bolu", CountryId=1 },
                new City() { CityId = 15, CityName = "Burdur", CountryId=1 },
                new City() { CityId = 16, CityName = "Bursa", CountryId=1 },
                new City() { CityId = 17, CityName = "Çanakkale", CountryId=1 },
                new City() { CityId = 18, CityName = "Çankırı", CountryId=1 },
                new City() { CityId = 19, CityName = "Çorum", CountryId=1 },
                new City() { CityId = 20, CityName = "Denizli", CountryId=1 },
                new City() { CityId = 21, CityName = "Diyarbakır", CountryId=1 },
                new City() { CityId = 22, CityName = "Edirne", CountryId=1 },
                new City() { CityId = 23, CityName = "Elazığ", CountryId=1 },
                new City() { CityId = 24, CityName = "Erzincan", CountryId=1 },
                new City() { CityId = 25, CityName = "Erzurum", CountryId=1 },
                new City() { CityId = 26, CityName = "Eskişehir", CountryId=1 },
                new City() { CityId = 27, CityName = "Gaziantep", CountryId=1 },
                new City() { CityId = 28, CityName = "Giresun", CountryId=1 },
                new City() { CityId = 29, CityName = "Gümüşhane", CountryId=1 },
                new City() { CityId = 30, CityName = "Hakkari", CountryId=1 },
                new City() { CityId = 31, CityName = "Hatay", CountryId=1 },
                new City() { CityId = 32, CityName = "Isparta", CountryId=1 },
                new City() { CityId = 33, CityName = "Mersin", CountryId=1 },
                new City() { CityId = 34, CityName = "İstanbul", CountryId=1 },
                new City() { CityId = 35, CityName = "İzmir", CountryId=1 },
                new City() { CityId = 36, CityName = "Kars", CountryId=1 },
                new City() { CityId = 37, CityName = "Kastamonu", CountryId=1 },
                new City() { CityId = 38, CityName = "Kayseri", CountryId=1 },
                new City() { CityId = 39, CityName = "Kırklareli", CountryId=1 },
                new City() { CityId = 40, CityName = "Kırşehir", CountryId=1 },
                new City() { CityId = 41, CityName = "Kocaeli", CountryId=1 },
                new City() { CityId = 42, CityName = "Konya", CountryId=1 },
                new City() { CityId = 43, CityName = "Kütahya", CountryId=1 },
                new City() { CityId = 44, CityName = "Malatya", CountryId=1 },
                new City() { CityId = 45, CityName = "Manisa", CountryId=1 },
                new City() { CityId = 46, CityName = "Kahramanmaraş", CountryId=1 },
                new City() { CityId = 47, CityName = "Mardin", CountryId=1 },
                new City() { CityId = 48, CityName = "Muğla", CountryId=1 },
                new City() { CityId = 49, CityName = "Muş", CountryId=1 },
                new City() { CityId = 50, CityName = "Nevşehir", CountryId=1 },
                new City() { CityId = 51, CityName = "Niğde", CountryId=1 },
                new City() { CityId = 52, CityName = "Ordu", CountryId=1 },
                new City() { CityId = 53, CityName = "Rize", CountryId=1 },
                new City() { CityId = 54, CityName = "Sakarya", CountryId=1 },
                new City() { CityId = 55, CityName = "Samsun", CountryId=1 },
                new City() { CityId = 56, CityName = "Siirt", CountryId=1 },
                new City() { CityId = 57, CityName = "Sinop", CountryId=1 },
                new City() { CityId = 58, CityName = "Sivas", CountryId=1 },
                new City() { CityId = 59, CityName = "Tekirdağ", CountryId=1 },
                new City() { CityId = 60, CityName = "Tokat", CountryId=1 },
                new City() { CityId = 61, CityName = "Trabzon", CountryId=1 },
                new City() { CityId = 62, CityName = "Tunceli", CountryId=1 },
                new City() { CityId = 63, CityName = "Şanlıurfa", CountryId=1 },
                new City() { CityId = 64, CityName = "Uşak", CountryId=1 },
                new City() { CityId = 65, CityName = "Van", CountryId=1 },
                new City() { CityId = 66, CityName = "Yozgat", CountryId=1 },
                new City() { CityId = 67, CityName = "Zonguldk", CountryId=1 },
                new City() { CityId = 68, CityName = "Aksaray", CountryId=1 },
                new City() { CityId = 69, CityName = "Bayburt", CountryId=1 },
                new City() { CityId = 70, CityName = "Karaman", CountryId=1 },
                new City() { CityId = 71, CityName = "Kırıkkale", CountryId=1 },
                new City() { CityId = 72, CityName = "Batman", CountryId=1 },
                new City() { CityId = 73, CityName = "Şırnak", CountryId=1 },
                new City() { CityId = 74, CityName = "Bartın", CountryId=1 },
                new City() { CityId = 75, CityName = "Ardahan", CountryId=1 },
                new City() { CityId = 76, CityName = "Iğdır", CountryId=1 },
                new City() { CityId = 77, CityName = "Yalova", CountryId=1 },
                new City() { CityId = 78, CityName = "Karabük", CountryId=1 },
                new City() { CityId = 79, CityName = "Kilis", CountryId=1 },
                new City() { CityId = 80, CityName = "Osmaniye", CountryId=1 },
                new City() { CityId = 81, CityName = "Düzce", CountryId=1 }
                );
        }
    }
}
