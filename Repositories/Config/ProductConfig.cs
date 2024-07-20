using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ProductName).IsRequired();
            builder.Property(p => p.Price).IsRequired();


            builder.HasData(
                new Product() { Id = 1, CategoryId = 2, ProductName = "Phone", Price = 15_000 ,ShowCase=false},
                new Product() { Id = 2, CategoryId = 2, ProductName = "Notebook", Price = 25_000, ShowCase = false },
                new Product() { Id = 3, CategoryId = 2, ProductName = "Keyboard", Price = 1_500, ShowCase = false },
                new Product() { Id = 4, CategoryId = 2, ProductName = "Monitor", Price = 5_000, ShowCase = false },
                new Product() { Id = 5, CategoryId = 2, ProductName = "Mouse", Price = 5000, ShowCase = false },
                new Product() { Id = 6, CategoryId = 1, ProductName = "Savaş Sanatı", Price = 50, ShowCase = true },
                new Product() { Id = 7, CategoryId = 1, ProductName = "Yüzbaşının Kızı", Price = 60, ShowCase = true },
                new Product() { Id = 8, CategoryId = 1, ProductName = "İtiraflar", Price = 500, ShowCase = true }
            
            );
        }



    }
}
