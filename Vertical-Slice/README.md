# Vertical Slice Architecture

## Опис
Vertical Slice організовує код по функціональних сценаріях (features) — кожен сценарій містить свій handler, DTOs, валідацію і тест. Це зменшує міжмодульні залежності та локалізує зміни.

## Структура
- `Features/Products/...` — окремі сценарії: GetAll, GetById, Create
- `Infrastructure/` — адаптери (InMemory репозиторій)
- `Domain/` — прості сутності

## Запуск
```powershell
cd "Vertical-Slice/VerticalSlice.API"
dotnet run
```

Swagger: https://localhost:5001/swagger

## Технології
- ASP.NET Core 8.0
- Entity Framework Core 8.0 з SQLite
- MediatR для handlers
- Swagger/OpenAPI

### Entity Framework Core
- **DbContext**: `AppDbContext` в Infrastructure
- **Repository**: `EfCoreProductRepository` для роботи з БД
- Також доступний `InMemoryProductRepository` для тестування
- База даних створюється автоматично при запуску
- Кожен slice використовує спільну інфраструктуру (можна мати окремі DbContext для різних slices)
