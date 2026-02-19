using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Domain.Interfaces;
using OnionArchitecture.Infrastructure.Repositories;

namespace OnionArchitecture.Infrastructure;

/// <summary>
/// Реєстрація Infrastructure Layer services
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        // Repositories
        services.AddSingleton<IProductRepository, ProductRepository>();
        
        // Тут можуть бути інші infrastructure сервіси:
        // - Database Context
        // - External API clients
        // - File system access
        // - Email services
        // тощо
        
        return services;
    }
}
