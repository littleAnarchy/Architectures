using ThreeLayerArchitecture.Models;

namespace ThreeLayerArchitecture.DataAccessLayer;

/// <summary>
/// DATA ACCESS LAYER - Інтерфейс для роботи з даними
/// Відповідає за прямий доступ до бази даних
/// </summary>
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}
