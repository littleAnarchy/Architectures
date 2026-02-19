using Microsoft.EntityFrameworkCore;
using ThreeLayerArchitecture.Models;

namespace ThreeLayerArchitecture.DataAccessLayer;

/// <summary>
/// DATA ACCESS LAYER - DbContext для Entity Framework Core
/// Управляє підключенням до бази даних та конфігурацією моделей
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Конфігурація моделі Product
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

        // Seed даних для демонстрації
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Ноутбук", Price = 25000, Description = "Потужний ноутбук", Stock = 10 },
            new Product { Id = 2, Name = "Миша", Price = 500, Description = "Бездротова миша", Stock = 50 },
            new Product { Id = 3, Name = "Клавіатура", Price = 1200, Description = "Механічна клавіатура", Stock = 30 }
        );
    }
}
