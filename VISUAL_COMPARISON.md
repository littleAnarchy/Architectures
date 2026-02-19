# Візуальне порівняння архітектур

## 1. Структура папок

### 3-Layer Architecture
```
3-Layer-Architecture/
├── Controllers/              # Presentation Layer
│   └── ProductsController.cs
├── BusinessLogicLayer/       # Business Logic Layer
│   ├── IProductService.cs
│   └── ProductService.cs
├── DataAccessLayer/          # Data Access Layer
│   ├── IProductRepository.cs
│   └── ProductRepository.cs
├── Models/
│   └── Product.cs
└── Program.cs
```

### Clean Architecture
```
Clean-Architecture/
├── CleanArchitecture.API/              # Presentation
│   ├── Controllers/
│   │   └── ProductsController.cs
│   └── Program.cs
├── CleanArchitecture.Application/      # Use Cases & Interfaces
│   ├── Products/
│   │   ├── Commands/
│   │   │   ├── CreateProductCommand.cs
│   │   │   ├── UpdateProductCommand.cs
│   │   │   └── DeleteProductCommand.cs
│   │   └── Queries/
│   │       ├── GetAllProductsQuery.cs
│   │       └── GetProductByIdQuery.cs
│   └── Interfaces/
│       └── IProductRepository.cs
├── CleanArchitecture.Domain/           # Core Business Logic
│   └── Entities/
│       └── Product.cs
└── CleanArchitecture.Infrastructure/   # Data & External Services
    ├── Persistence/
    │   └── ProductRepository.cs
    └── DependencyInjection.cs
```

### Onion Architecture
```
Onion-Architecture/
├── OnionArchitecture.API/              # Outer Ring
│   ├── Controllers/
│   │   └── ProductsController.cs
│   └── Program.cs
├── OnionArchitecture.Application/      # Ring 3
│   ├── Services/
│   │   └── ProductService.cs
│   └── Interfaces/
│       └── IProductService.cs
├── OnionArchitecture.Domain/           # Rings 1-2
│   ├── Entities/
│   │   └── Product.cs           # Ring 1 (Core)
│   ├── Services/
│   │   └── ProductDomainService.cs  # Ring 2
│   └── Interfaces/
│       └── IProductRepository.cs
└── OnionArchitecture.Infrastructure/   # Outer Ring
    ├── Repositories/
    │   └── ProductRepository.cs
    └── DependencyInjection.cs
```

---

## 2. Діаграми потоку даних

### 3-Layer: Створення продукту
```
HTTP POST /api/products
     ↓
[ProductsController]
     ↓ calls
[ProductService.CreateProductAsync()]
     ↓ validates & calls
[ProductRepository.AddAsync()]
     ↓ saves to
[Database/Memory]
     ↓ returns
Product ⟵ ⟵ ⟵ ⟵ ⟵
```

### Clean: Створення продукту
```
HTTP POST /api/products
     ↓
[ProductsController]
     ↓ sends command via MediatR
[CreateProductCommand]
     ↓ handled by
[CreateProductHandler]
     ↓ creates Domain Entity
[Product Entity] (self-validates)
     ↓ uses interface
[IProductRepository] (defined in Application)
     ↓ implemented in Infrastructure
[ProductRepository.AddAsync()]
     ↓ returns
Product ⟵ ⟵ ⟵ ⟵ ⟵
```

### Onion: Створення продукту
```
HTTP POST /api/products
     ↓
[ProductsController]
     ↓ calls
[IProductService]
     ↓ implemented by
[ProductService] (Application)
     ↓ validates with
[ProductDomainService] (Domain)
     ↓ creates
[Product Entity] (Domain Core)
     ↓ saves via interface
[IProductRepository] (Domain Interface)
     ↓ implemented in
[ProductRepository] (Infrastructure)
     ↓ returns
Product ⟵ ⟵ ⟵ ⟵ ⟵
```

---

## 3. Де живуть валідації?

### 3-Layer
```csharp
// ❌ Валідація в Service Layer
public class ProductService
{
    public async Task<Product> CreateProductAsync(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Name required");
        
        if (product.Price <= 0)
            throw new ArgumentException("Price must be positive");
            
        return await _repository.AddAsync(product);
    }
}
```

### Clean Architecture
```csharp
// ✅ Валідація в Domain Entity
public class Product
{
    private string _name = string.Empty;
    
    public Product(string name, decimal price, ...)
    {
        SetName(name);  // Валідує!
        SetPrice(price); // Валідує!
    }
    
    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name required");
        _name = name;
    }
}

// Command Handler просто використовує Entity
public class CreateProductHandler
{
    public async Task<Product> Handle(...)
    {
        var product = new Product(...); // Валідація тут!
        return await _repository.AddAsync(product);
    }
}
```

### Onion Architecture
```csharp
// ✅ Валідація розподілена між Domain Entity і Domain Service
public class Product
{
    // Проста валідація в Entity
    public void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be positive");
        _price = price;
    }
}

public class ProductDomainService : IProductDomainService
{
    // Складна бізнес-валідація в Domain Service
    public bool ValidateProductData(string name, decimal price, int stock)
    {
        if (name.Length < 3) return false;
        if (price > 1000000) return false; // Business rule!
        return true;
    }
}

// Application Service координує
public class ProductService
{
    public async Task<Product> CreateProductAsync(...)
    {
        // Використовуємо Domain Service
        if (!_domainService.ValidateProductData(name, price, stock))
            throw new ArgumentException("Invalid data");
            
        // Entity валідує себе
        var product = new Product(name, price, ...);
        return await _repository.AddAsync(product);
    }
}
```

---

## 4. Тестування

### 3-Layer - Складно тестувати
```csharp
// Щоб протестувати Service, потрібен Repository
// Щоб протестувати Repository, потрібна БД
[Test]
public async Task CreateProduct_ShouldValidate()
{
    // Потрібен mock repository
    var mockRepo = new Mock<IProductRepository>();
    var service = new ProductService(mockRepo.Object);
    
    // Test...
}
```

### Clean - Легко тестувати
```csharp
// Domain Entity тестується без будь-яких залежностей!
[Test]
public void Product_SetPrice_ShouldValidate()
{
    var product = new Product("Test", 100, "Desc", 10);
    
    Assert.Throws<ArgumentException>(() => 
        product.SetPrice(-1)); // Немає залежностей!
}

// Handler тестується з mock repository
[Test]
public async Task CreateProduct_ShouldCreate()
{
    var mockRepo = new Mock<IProductRepository>();
    var handler = new CreateProductHandler(mockRepo.Object);
    
    // Test...
}
```

### Onion - Найлегше тестувати
```csharp
// Domain Entity - без залежностей
[Test]
public void Product_ReduceStock_ShouldValidate()
{
    var product = new Product("Test", 100, "Desc", 10);
    
    Assert.Throws<InvalidOperationException>(() => 
        product.ReduceStock(20)); // Недостатньо!
}

// Domain Service - без залежностей
[Test]
public void DomainService_ValidateData_ShouldWork()
{
    var domainService = new ProductDomainService();
    
    var result = domainService.ValidateProductData("AB", 100, 10);
    Assert.False(result); // Назва занадто коротка
}

// Application Service - з mocks
[Test]
public async Task ProductService_Create_ShouldUseAllLayers()
{
    var mockRepo = new Mock<IProductRepository>();
    var mockDomainService = new Mock<IProductDomainService>();
    var appService = new ProductService(mockRepo.Object, 
                                       mockDomainService.Object);
    
    // Test coordination...
}
```

---

## 5. Залежності між компонентами

### 3-Layer
```
┌─────────────────────┐
│   ProductsController │ ───┐
└─────────────────────┘    │
                           │ depends on
┌─────────────────────┐    │
│   ProductService     │ ◄──┘
└─────────────────────┘
          │
          │ depends on
          ▼
┌─────────────────────┐
│  ProductRepository   │
└─────────────────────┘
          │
          ▼
      Database
```

### Clean Architecture
```
┌─────────────────────┐
│   ProductsController │ ─────┐
└─────────────────────┘      │
                              │
┌─────────────────────┐      │
│  ProductRepository   │ ─────┤
└─────────────────────┘      │ depends on
                              │
                              ▼
┌──────────────────────────────┐
│   Application Layer          │
│  - Commands/Queries          │
│  - IProductRepository (i)    │
└─────────────┬────────────────┘
              │ depends on
              ▼
┌──────────────────────────────┐
│      Domain Layer            │
│     - Product Entity         │
│   (NO DEPENDENCIES!)         │
└──────────────────────────────┘
```

### Onion Architecture
```
┌─────────────────────┐
│   ProductsController │ ─────┐
└─────────────────────┘      │
                              │
┌─────────────────────┐      │
│  ProductRepository   │ ─────┤
└─────────────────────┘      │
                              │
                              ▼
┌──────────────────────────────┐
│   Application Services       │
│    - ProductService          │
└─────────────┬────────────────┘
              │
              ▼
┌──────────────────────────────┐
│      Domain Services         │
│   - ProductDomainService     │
│   - IProductRepository (i)   │
└─────────────┬────────────────┘
              │
              ▼
┌──────────────────────────────┐
│       Domain Model           │
│     - Product Entity         │
│    (PURE - NO DEPS!)         │
└──────────────────────────────┘
```

---

## 6. Коли використати кожну?

### Простий проект (100-1000 LOC)
```
✅ 3-Layer: Ideal!
⚠️ Clean:   Можливо, але надмірно
❌ Onion:   Определенно надмірно
```

### Середній проект (1000-10000 LOC)
```
⚠️ 3-Layer: Можливо, але може стати складно підтримувати
✅ Clean:   Ideal!
⚠️ Onion:   Якщо складний domain
```

### Великий enterprise проект (10000+ LOC)
```
❌ 3-Layer: Буде важко підтримувати
✅ Clean:   Добрий вибір
✅ Onion:   Ideal для DDD!
```

---

## 7. Приклад зміни: додати нову валідацію

### Завдання: "Ціна не може бути більше 100000"

#### 3-Layer
```csharp
// Змінюємо ProductService.cs
public async Task<Product> CreateProductAsync(Product product)
{
    if (product.Price <= 0)
        throw new ArgumentException("Price must be positive");
    
    if (product.Price > 100000) // ← НОВЕ
        throw new ArgumentException("Price too high");
        
    return await _repository.AddAsync(product);
}

// ⚠️ Треба змінити і в UpdateProductAsync()!
```

#### Clean
```csharp
// Змінюємо Product.cs (Domain)
public void SetPrice(decimal price)
{
    if (price <= 0)
        throw new ArgumentException("Price must be positive");
    
    if (price > 100000) // ← НОВЕ
        throw new ArgumentException("Price too high");
        
    _price = price;
}

// ✅ Автоматично працює всюди де викликається SetPrice()
```

#### Onion
```csharp
// Змінюємо ProductDomainService.cs
public bool ValidateProductData(string name, decimal price, int stock)
{
    if (price <= 0) return false;
    if (price > 100000) return false; // ← БУЛО 1000000, стало 100000
    // ...
}

// ✅ Бізнес-правило в одному місці
// ✅ ProductService автоматично використовує нове правило
```

---

## Висновок

| Метрика | 3-Layer | Clean | Onion |
|---------|---------|-------|-------|
| **Простота** | 🟢🟢🟢 | 🟡🟡 | 🔴 |
| **Гнучкість** | 🔴 | 🟢🟢🟢 | 🟢🟢🟢 |
| **Тестованість** | 🟡 | 🟢🟢🟢 | 🟢🟢🟢 |
| **Підтримка** | 🟡 | 🟢🟢 | 🟢🟢🟢 |
| **Швидкість розробки** | 🟢🟢🟢 | 🟡🟡 | 🔴 |
| **DDD підтримка** | 🔴 | 🟡 | 🟢🟢🟢 |

🟢 - Відмінно | 🟡 - Добре | 🔴 - Погано
