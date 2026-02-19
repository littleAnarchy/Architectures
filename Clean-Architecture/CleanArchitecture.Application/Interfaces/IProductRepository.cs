using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Interfaces;

/// <summary>
/// APPLICATION LAYER - Інтерфейс репозиторію
/// Визначається в Application, але реалізується в Infrastructure
/// Це інверсія залежностей - Application не залежить від Infrastructure
/// </summary>
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
