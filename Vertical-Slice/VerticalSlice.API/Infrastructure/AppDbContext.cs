using Microsoft.EntityFrameworkCore;
using VerticalSlice.Domain;

namespace VerticalSlice.Infrastructure;

/// <summary>
/// DbContext для Entity Framework Core
/// У Vertical Slice кожен slice може мати свій окремий DbContext якщо потрібно,
/// або використовувати спільний як в цьому прикладі
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
            new Product { Id = 1, Name = "Монітор", Price = 8000, Description = "4K", Stock = 5 },
            new Product { Id = 2, Name = "Клавіатура", Price = 1200, Description = "Механічна", Stock = 30 }
        );
    }
}
