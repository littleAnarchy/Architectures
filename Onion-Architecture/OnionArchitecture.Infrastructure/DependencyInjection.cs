using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Domain.Interfaces;
using OnionArchitecture.Infrastructure.Persistence;
using OnionArchitecture.Infrastructure.Repositories;

namespace OnionArchitecture.Infrastructure;

/// <summary>
/// Реєстрація Infrastructure Layer services
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Database Context
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        
        // Тут можуть бути інші infrastructure сервіси:
        // - External API clients
        // - File system access
        // - Email services
        // тощо
        
        return services;
    }
}
