using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Config;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace Repositories;

public class RepositoryContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<CouponCode> CouponCodes { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public RepositoryContext(DbContextOptions<RepositoryContext> options)
    : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfiguration(new ProductConfig());
        //modelBuilder.ApplyConfiguration(new CategoryConfig());
        modelBuilder.Entity<PrdImage>()
                .HasOne(i => i.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.ProductId);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
