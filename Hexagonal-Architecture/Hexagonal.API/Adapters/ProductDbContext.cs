using Microsoft.EntityFrameworkCore;
using HexagonalArchitecture.Domain;

namespace HexagonalArchitecture.Adapters;

/// <summary>
/// Adapter - DbContext для Entity Framework Core
/// Реалізує технічні деталі зберігання даних
/// </summary>
public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
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
            new Product { Id = 1, Name = "Ноутбук", Price = 25000, Description = "Потужний", Stock = 10 },
            new Product { Id = 2, Name = "Миша", Price = 500, Description = "Бездротова", Stock = 50 }
        );
    }
}
