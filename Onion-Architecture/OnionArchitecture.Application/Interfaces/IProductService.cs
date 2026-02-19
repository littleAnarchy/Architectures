using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces;

/// <summary>
/// APPLICATION SERVICES LAYER (Ring 3) - Application Service Interface
/// Визначає контракти для сценаріїв використання (use cases)
/// Координує роботу Domain Services та Repositories
/// </summary>
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);
    Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Product> CreateProductAsync(string name, decimal price, string description, int stock, CancellationToken cancellationToken = default);
    Task<Product> UpdateProductAsync(int id, string name, decimal price, string description, int stock, CancellationToken cancellationToken = default);
    Task DeleteProductAsync(int id, CancellationToken cancellationToken = default);
    Task ReduceStockAsync(int productId, int quantity, CancellationToken cancellationToken = default);
    Task<decimal> GetProductTotalValueAsync(int productId, CancellationToken cancellationToken = default);
}
