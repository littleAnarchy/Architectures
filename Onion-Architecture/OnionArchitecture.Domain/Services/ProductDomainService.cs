using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Domain.Services;

/// <summary>
/// DOMAIN SERVICES LAYER (Ring 2) - Domain Service
/// Містить бізнес-логіку, яка не відноситься до конкретної сутності
/// або координує роботу кількох сутностей
/// </summary>
public interface IProductDomainService
{
    bool ValidateProductData(string name, decimal price, int stock);
    decimal CalculateTotalValue(Product product);
    bool IsLowStock(Product product, int threshold = 10);
}

public class ProductDomainService : IProductDomainService
{
    public bool ValidateProductData(string name, decimal price, int stock)
    {
        // Складна бізнес-логіка валідації, яка може використовувати зовнішні правила
        if (string.IsNullOrWhiteSpace(name)) return false;
        if (price <= 0) return false;
        if (stock < 0) return false;

        // Додаткові бізнес-правила
        if (name.Length < 3) return false;
        if (price > 1000000) return false; // Максимальна ціна

        return true;
    }

    public decimal CalculateTotalValue(Product product)
    {
        // Бізнес-логіка розрахунку загальної вартості товару на складі
        return product.Price * product.Stock;
    }

    public bool IsLowStock(Product product, int threshold = 10)
    {
        // Бізнес-правило для визначення низького рівня запасів
        return product.Stock <= threshold && product.Stock > 0;
    }
}
