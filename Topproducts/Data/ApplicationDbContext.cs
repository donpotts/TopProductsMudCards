using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Topproducts.Models;
using Topproducts.Shared.Models;

namespace Topproducts.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Product> Product => Set<Product>();
    public DbSet<Vendor> Vendor => Set<Vendor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(e => e.Price)
            .HasConversion<double>();
        modelBuilder.Entity<Product>()
            .Property(e => e.Price)
            .HasPrecision(19, 4);
        modelBuilder.Entity<Product>()
            .Property(e => e.Rating)
            .HasConversion<double>();
        modelBuilder.Entity<Product>()
            .Property(e => e.Rating)
            .HasPrecision(19, 4);
        modelBuilder.Entity<Product>()
            .HasOne(x => x.Vendor);
    }
}
