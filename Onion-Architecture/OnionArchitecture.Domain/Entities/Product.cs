namespace OnionArchitecture.Domain.Entities;

/// <summary>
/// CORE (Ядро цибулі) - Domain Entity
/// Центр архітектури, містить бізнес-логіку сутності
/// НЕ МАЄ ЗАЛЕЖНОСТЕЙ взагалі
/// </summary>
public class Product
{
    public int Id { get; set; }
    private string _name = string.Empty;
    private decimal _price;
    private int _stock;

    public string Name 
    { 
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва не може бути порожньою");
            _name = value;
        }
    }

    public decimal Price 
    { 
        get => _price;
        private set
        {
            if (value <= 0)
                throw new ArgumentException("Ціна має бути більше 0");
            _price = value;
        }
    }

    public string Description { get; set; } = string.Empty;

    public int Stock 
    { 
        get => _stock;
        private set
        {
            if (value < 0)
                throw new ArgumentException("Кількість не може бути від'ємною");
            _stock = value;
        }
    }

    // Domain Events (можуть бути додані пізніше)
    // public List<IDomainEvent> DomainEvents { get; } = new();

    private Product() { }

    public Product(string name, decimal price, string description, int stock)
    {
        Name = name;
        Price = price;
        Description = description;
        Stock = stock;
    }

    // Domain methods - бізнес-логіка на рівні сутності
    public void UpdateDetails(string name, decimal price, string description)
    {
        Name = name;
        Price = price;
        Description = description;
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Кількість має бути більше 0");
        
        Stock += quantity;
    }

    public void ReduceStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Кількість має бути більше 0");
        
        if (!CanReduceStock(quantity))
            throw new InvalidOperationException($"Недостатньо товару на складі. Доступно: {Stock}, запитано: {quantity}");
        
        Stock -= quantity;
        
        // Тут можна додати Domain Event
        // DomainEvents.Add(new StockReducedEvent(Id, quantity));
    }

    public bool CanReduceStock(int quantity)
    {
        return Stock >= quantity;
    }

    public bool IsInStock()
    {
        return Stock > 0;
    }

    public void SetStock(int newStock)
    {
        Stock = newStock;
    }
}
