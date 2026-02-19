using ThreeLayerArchitecture.Models;

namespace ThreeLayerArchitecture.DataAccessLayer;

/// <summary>
/// DATA ACCESS LAYER - Реалізація репозиторію
/// В реальному проєкті тут був би Entity Framework або інший ORM
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Ноутбук", Price = 25000, Description = "Потужний ноутбук", Stock = 10 },
        new Product { Id = 2, Name = "Миша", Price = 500, Description = "Бездротова миша", Stock = 50 },
        new Product { Id = 3, Name = "Клавіатура", Price = 1200, Description = "Механічна клавіатура", Stock = 30 }
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
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);
        return Task.FromResult(product);
    }

    public Task<Product> UpdateAsync(Product product)
    {
        var existing = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existing != null)
        {
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;
            existing.Stock = product.Stock;
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
