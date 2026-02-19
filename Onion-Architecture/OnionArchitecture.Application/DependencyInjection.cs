using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Interfaces;
using OnionArchitecture.Application.Services;
using OnionArchitecture.Domain.Services;

namespace OnionArchitecture.Application;

/// <summary>
/// Реєстрація Application Layer services
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        // Application Services
        services.AddScoped<IProductService, ProductService>();
        
        // Domain Services
        services.AddScoped<IProductDomainService, ProductDomainService>();
        
        return services;
    }
}
