using VerticalSlice.Domain;

namespace VerticalSlice.Infrastructure;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Монітор", Price = 8000, Description = "4K", Stock = 5 },
        new Product { Id = 2, Name = "Клавіатура", Price = 1200, Description = "Механічна", Stock = 30 }
    };

    public Task<IEnumerable<Product>> GetAllAsync() => Task.FromResult<IEnumerable<Product>>(_products);
    public Task<Product?> GetByIdAsync(int id) => Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
    public Task<Product> AddAsync(Product product)
    {
        product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
        _products.Add(product);
        return Task.FromResult(product);
    }
}
