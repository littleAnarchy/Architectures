using HexagonalArchitecture.Domain;

namespace HexagonalArchitecture.Ports;

/// <summary>
/// Port - абстракція для доступу до даних (в Hexagonal визначається в ядрі/ports)
/// </summary>
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}
