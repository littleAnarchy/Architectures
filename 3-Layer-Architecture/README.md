# 3-Layer Architecture (Трирівнева архітектура)

## Опис
Класична трирівнева архітектура - це найпростіший та найпоширеніший патерн. Вона складається з трьох шарів:

## Структура шарів

### 1. **Presentation Layer (Шар представлення)** 
- **Папка**: `Controllers/`
- **Відповідальність**: Обробка HTTP запитів/відповідей, маршрутизація
- **Залежності**: → Business Logic Layer

### 2. **Business Logic Layer (Шар бізнес-логіки)**
- **Папка**: `BusinessLogicLayer/`
- **Відповідальність**: Бізнес-правила, валідація, обчислення
- **Залежності**: → Data Access Layer

### 3. **Data Access Layer (Шар доступу до даних)**
- **Папка**: `DataAccessLayer/`
- **Відповідальність**: Робота з базою даних, CRUD операції
- **Залежності**: Немає (або тільки БД)

## Потік залежностей
```
Controllers → Services → Repositories → Database
(Presentation) → (Business Logic) → (Data Access)
```

## Переваги
✅ Проста для розуміння  
✅ Легко впроваджувати  
✅ Підходить для малих та середніх проектів  
✅ Чітке розділення відповідальностей  

## Недоліки
❌ Жорсткі залежності між шарами  
❌ Важко тестувати (сильна зв'язаність)  
❌ БД знаходиться в центрі архітектури  
❌ Важко змінювати один шар без впливу на інші  

## Коли використовувати
- Прості CRUD додатки
- Малі проекти
- Прототипи
- Коли потрібна швидка розробка

## Запуск
```bash
cd 3-Layer-Architecture
dotnet run
```

Swagger UI: https://localhost:5001/swagger

## Технології
- ASP.NET Core 8.0
- Entity Framework Core 8.0 з SQLite
- Swagger/OpenAPI

### Entity Framework Core
Проект використовує EF Core для роботи з SQLite базою даних:
- **DbContext**: `AppDbContext` в `DataAccessLayer/`
- **Repository**: `ProductRepository` використовує `AppDbContext` для всіх операцій з БД
- **Connection String**: в `appsettings.json` (`Data Source=products.db`)
- База даних створюється автоматично при першому запуску з seed даними
