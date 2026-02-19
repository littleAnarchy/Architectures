using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Ports;

namespace HexagonalArchitecture.Adapters;

/// <summary>
/// Adapter - реалізація порту (In-memory DB)
/// </summary>
public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Ноутбук", Price = 25000, Description = "Потужний", Stock = 10 },
        new Product { Id = 2, Name = "Миша", Price = 500, Description = "Бездротова", Stock = 50 }
    };

    public Task<IEnumerable<Product>> GetAllAsync() => Task.FromResult<IEnumerable<Product>>(_products);

    public Task<Product?> GetByIdAsync(int id) => Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

    public Task<Product> AddAsync(Product product)
    {
        product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
        _products.Add(product);
        return Task.FromResult(product);
    }

    public Task<Product> UpdateAsync(Product product)
    {
        var idx = _products.FindIndex(p => p.Id == product.Id);
        if (idx >= 0) _products[idx] = product;
        return Task.FromResult(product);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var p = _products.FirstOrDefault(x => x.Id == id);
        if (p == null) return Task.FromResult(false);
        _products.Remove(p);
        return Task.FromResult(true);
    }
}
