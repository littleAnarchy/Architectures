using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Domain.Interfaces;

/// <summary>
/// DOMAIN LAYER - Repository Interface
/// В Onion Architecture інтерфейси репозиторіїв визначаються в Domain
/// (на відміну від Clean Architecture, де вони в Application)
/// </summary>
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
