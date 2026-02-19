using ThreeLayerArchitecture.Models;

namespace ThreeLayerArchitecture.BusinessLogicLayer;

/// <summary>
/// BUSINESS LOGIC LAYER - Інтерфейс бізнес-логіки
/// Містить бізнес-правила та валідацію
/// </summary>
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> CheckAvailabilityAsync(int productId, int quantity, CancellationToken cancellationToken = default);
}
