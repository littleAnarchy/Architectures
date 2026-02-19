using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Persistence;

/// <summary>
/// INFRASTRUCTURE LAYER - Реалізація репозиторію
/// Містить технічні деталі роботи з даними
/// Залежить від Application (через інтерфейс), але Application НЕ залежить від Infrastructure
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product("Ноутбук", 25000, "Потужний ноутбук для роботи", 10) { Id = 1 },
        new Product("Миша", 500, "Бездротова миша", 50) { Id = 2 },
        new Product("Клавіатура", 1200, "Механічна клавіатура", 30) { Id = 3 }
    };

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(_products);
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    public Task<Product> AddAsync(Product product)
    {
        product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
        _products.Add(product);
        return Task.FromResult(product);
    }

    public Task<Product> UpdateAsync(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index >= 0)
        {
            _products[index] = product;
        }
        return Task.FromResult(product);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}
