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
