using OnionArchitecture.Application.Interfaces;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.Interfaces;
using OnionArchitecture.Domain.Services;

namespace OnionArchitecture.Application.Services;

/// <summary>
/// APPLICATION SERVICES LAYER (Ring 3) - Application Service Implementation
/// Оркеструє Domain Services, Entities та Repositories
/// Реалізує бізнес use cases
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IProductDomainService _domainService;

    public ProductService(
        IProductRepository repository,
        IProductDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }

    public Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default) =>
        _repository.GetAllAsync(cancellationToken);

    public async Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetByIdAsync(id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Продукт з ID {id} не знайдено");
            
        return product;
    }

    public async Task<Product> CreateProductAsync(string name, decimal price, string description, int stock, CancellationToken cancellationToken = default)
    {
        // Використовуємо Domain Service для валідації
        if (!_domainService.ValidateProductData(name, price, stock))
            throw new ArgumentException("Невалідні дані продукту");

        // Створюємо сутність (Domain Entity відповідає за власну валідацію)
        var product = new Product(name, price, description, stock);

        // Зберігаємо через репозиторій
        return await _repository.AddAsync(product, cancellationToken);
    }

    public async Task<Product> UpdateProductAsync(int id, string name, decimal price, string description, int stock, CancellationToken cancellationToken = default)
    {
        // Перевіряємо існування
        var product = await GetProductByIdAsync(id, cancellationToken);

        // Використовуємо Domain Service для валідації
        if (!_domainService.ValidateProductData(name, price, stock))
            throw new ArgumentException("Невалідні дані продукту");

        // Оновлюємо через методи Domain Entity
        product.UpdateDetails(name, price, description);
        product.SetStock(stock);

        // Перевіряємо чи низький рівень запасів (business logic)
        if (_domainService.IsLowStock(product))
        {
            // Тут можна додати логіку сповіщень, логування тощо
            Console.WriteLine($"Увага: Низький рівень запасів для продукту {product.Name}");
        }

        return await _repository.UpdateAsync(product, cancellationToken);
    }

    public async Task DeleteProductAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!await _repository.ExistsAsync(id, cancellationToken))
            throw new KeyNotFoundException($"Продукт з ID {id} не знайдено");

        await _repository.DeleteAsync(id, cancellationToken);
    }

    public async Task ReduceStockAsync(int productId, int quantity, CancellationToken cancellationToken = default)
    {
        var product = await GetProductByIdAsync(productId, cancellationToken);

        // Domain Entity сам валідує і зменшує запаси
        product.ReduceStock(quantity);

        // Перевіряємо стан після зміни
        if (_domainService.IsLowStock(product))
        {
            Console.WriteLine($"Увага: Низький рівень запасів для продукту {product.Name}");
        }

        await _repository.UpdateAsync(product, cancellationToken);
    }

    public async Task<decimal> GetProductTotalValueAsync(int productId, CancellationToken cancellationToken = default)
    {
        var product = await GetProductByIdAsync(productId, cancellationToken);
        
        // Використовуємо Domain Service для обчислень
        return _domainService.CalculateTotalValue(product);
    }
}
