using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Interfaces;

/// <summary>
/// APPLICATION LAYER - Інтерфейс репозиторію
/// Визначається в Application, але реалізується в Infrastructure
/// Це інверсія залежностей - Application не залежить від Infrastructure
/// </summary>
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}
