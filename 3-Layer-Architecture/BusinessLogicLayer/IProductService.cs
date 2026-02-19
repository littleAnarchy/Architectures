using ThreeLayerArchitecture.Models;

namespace ThreeLayerArchitecture.BusinessLogicLayer;

/// <summary>
/// BUSINESS LOGIC LAYER - Інтерфейс бізнес-логіки
/// Містить бізнес-правила та валідацію
/// </summary>
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
    Task<bool> CheckAvailabilityAsync(int productId, int quantity);
}
