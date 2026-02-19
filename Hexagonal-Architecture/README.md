# Hexagonal Architecture (Ports & Adapters)

## Опис
Hexagonal (Ports & Adapters) концентрує бізнес-логіку в середині, а всі I/O (БД, API, UI) — за допомогою портів і адаптерів. Це дозволяє легко підміняти адаптери без змін в ядрі.

## Структура
- `Domain/` — сутності
- `Ports/` — інтерфейси (порти)
- `Adapters/` — реалізації портів (наприклад, InMemory, EFCore, API clients)
- `Controllers/` — зовнішній шар

## Запуск
```powershell
cd "Hexagonal-Architecture/Hexagonal.API"
dotnet run
```

Swagger: https://localhost:5001/swagger

## Технології
- ASP.NET Core 8.0
- Entity Framework Core 8.0 з SQLite
- Swagger/OpenAPI

### Entity Framework Core
- **DbContext**: `ProductDbContext` в Adapters
- **Adapter**: `EfCoreProductRepository` реалізує порт `IProductRepository`
- Також доступний `InMemoryProductRepository` — можна легко переключатися між адаптерами
- База даних створюється автоматично при запуску
- Демонструє гнучкість Hexagonal Architecture через змінні адаптери
