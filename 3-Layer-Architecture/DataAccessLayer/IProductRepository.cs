using ThreeLayerArchitecture.Models;

namespace ThreeLayerArchitecture.DataAccessLayer;

/// <summary>
/// DATA ACCESS LAYER - Інтерфейс для роботи з даними
/// Відповідає за прямий доступ до бази даних
/// </summary>
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
