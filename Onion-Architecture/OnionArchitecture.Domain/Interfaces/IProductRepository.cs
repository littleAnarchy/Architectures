using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Domain.Interfaces;

/// <summary>
/// DOMAIN LAYER - Repository Interface
/// В Onion Architecture інтерфейси репозиторіїв визначаються в Domain
/// (на відміну від Clean Architecture, де вони в Application)
/// </summary>
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
