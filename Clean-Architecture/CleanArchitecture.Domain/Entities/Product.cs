namespace CleanArchitecture.Domain.Entities;

/// <summary>
/// DOMAIN LAYER - Сутність продукту
/// Центр архітектури, не має залежностей
/// Містить тільки бізнес-логіку, що стосується самої сутності
/// </summary>
public class Product
{
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string Description { get; set; } = string.Empty;
    public int Stock { get; private set; }

    // Приватний конструктор для забезпечення валідності
    private Product() { }

    public Product(string name, decimal price, string description, int stock)
    {
        SetName(name);
        SetPrice(price);
        Description = description;
        SetStock(stock);
    }

    // Бізнес-логіка в самій сутності
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Назва не може бути порожньою", nameof(name));
            
        Name = name;
    }

    public void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Ціна має бути більше 0", nameof(price));
            
        Price = price;
    }

    public void SetStock(int stock)
    {
        if (stock < 0)
            throw new ArgumentException("Кількість не може бути від'ємною", nameof(stock));
            
        Stock = stock;
    }

    public bool IsAvailable(int quantity)
    {
        return Stock >= quantity;
    }

    public void ReduceStock(int quantity)
    {
        if (!IsAvailable(quantity))
            throw new InvalidOperationException("Недостатньо товару на складі");
            
        Stock -= quantity;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Кількість має бути більше 0", nameof(quantity));
            
        Stock += quantity;
    }
}
