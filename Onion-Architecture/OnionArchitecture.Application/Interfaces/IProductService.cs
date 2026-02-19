using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Interfaces;

/// <summary>
/// APPLICATION SERVICES LAYER (Ring 3) - Application Service Interface
/// Визначає контракти для сценаріїв використання (use cases)
/// Координує роботу Domain Services та Repositories
/// </summary>
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task<Product> CreateProductAsync(string name, decimal price, string description, int stock);
    Task<Product> UpdateProductAsync(int id, string name, decimal price, string description, int stock);
    Task DeleteProductAsync(int id);
    Task ReduceStockAsync(int productId, int quantity);
    Task<decimal> GetProductTotalValueAsync(int productId);
}
