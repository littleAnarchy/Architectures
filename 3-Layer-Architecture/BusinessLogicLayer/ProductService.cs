using ThreeLayerArchitecture.DataAccessLayer;
using ThreeLayerArchitecture.Models;

namespace ThreeLayerArchitecture.BusinessLogicLayer;

/// <summary>
/// BUSINESS LOGIC LAYER - Реалізація бізнес-логіки
/// Тут знаходяться всі бізнес-правила, валідація, обчислення
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        // Бізнес-правила: валідація
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Назва продукту не може бути порожньою");

        if (product.Price <= 0)
            throw new ArgumentException("Ціна має бути більше 0");

        if (product.Stock < 0)
            throw new ArgumentException("Кількість не може бути від'ємною");

        return await _repository.AddAsync(product);
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        // Бізнес-правила: перевірка існування
        var existing = await _repository.GetByIdAsync(product.Id);
        if (existing == null)
            throw new KeyNotFoundException($"Продукт з ID {product.Id} не знайдено");

        // Валідація
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Назва продукту не може бути порожньою");

        if (product.Price <= 0)
            throw new ArgumentException("Ціна має бути більше 0");

        return await _repository.UpdateAsync(product);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
            throw new KeyNotFoundException($"Продукт з ID {id} не знайдено");

        return await _repository.DeleteAsync(id);
    }

    public async Task<bool> CheckAvailabilityAsync(int productId, int quantity)
    {
        // Бізнес-логіка: перевірка доступності
        var product = await _repository.GetByIdAsync(productId);
        if (product == null)
            return false;

        return product.Stock >= quantity;
    }
}
