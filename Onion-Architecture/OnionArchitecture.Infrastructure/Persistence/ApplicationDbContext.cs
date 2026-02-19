using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Infrastructure.Persistence;

/// <summary>
/// INFRASTRUCTURE LAYER - DbContext для Entity Framework Core
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18,2)");
            
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
        });

        // Seed даних
        modelBuilder.Entity<Product>().HasData(
            new Product("Ноутбук", 25000, "Потужний ноутбук", 10) { Id = 1 },
            new Product("Миша", 500, "Бездротова миша", 50) { Id = 2 },
            new Product("Клавіатура", 1200, "Механічна клавіатура", 30) { Id = 3 },
            new Product("Монітор", 8000, "4K монітор", 5) { Id = 4 }
        );
    }
}
