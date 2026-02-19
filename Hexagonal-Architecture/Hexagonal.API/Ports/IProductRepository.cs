using HexagonalArchitecture.Domain;

namespace HexagonalArchitecture.Ports;

/// <summary>
/// Port - абстракція для доступу до даних (в Hexagonal визначається в ядрі/ports)
/// </summary>
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
