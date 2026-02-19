# Швидкий старт і приклади API

## 🚀 Як запустити проекти

### Опція 1: Запустити всі проекти з різних терміналів

#### Термінал 1 - 3-Layer Architecture
```powershell
cd "c:\Users\VladyslavMakukh\Desktop\Junk\Architectures\3-Layer-Architecture"
dotnet run --urls "http://localhost:5001"
```
Swagger: http://localhost:5001/swagger

#### Термінал 2 - Clean Architecture
```powershell
cd "c:\Users\VladyslavMakukh\Desktop\Junk\Architectures\Clean-Architecture\CleanArchitecture.API"
dotnet run --urls "http://localhost:5002"
```
Swagger: http://localhost:5002/swagger

#### Термінал 3 - Onion Architecture
```powershell
cd "c:\Users\VladyslavMakukh\Desktop\Junk\Architectures\Onion-Architecture\OnionArchitecture.API"
dotnet run --urls "http://localhost:5003"
```
Swagger: http://localhost:5003/swagger

---

### Опція 2: Запустити один проект
```powershell
# Виберіть один з проектів
cd "c:\Users\VladyslavMakukh\Desktop\Junk\Architectures\3-Layer-Architecture"
dotnet run
```

---

## 📝 Приклади запитів (для прикладів у цьому репозиторії)

### 1. Отримати всі продукти
```powershell
# PowerShell
Invoke-RestMethod -Uri "http://localhost:5001/api/products" -Method Get | ConvertTo-Json -Depth 3

# Або через curl
curl http://localhost:5001/api/products
```

**Очікувана відповідь:**
```json
[
  {
    "id": 1,
    "name": "Ноутбук",
    "price": 25000,
    "description": "Потужний ноутбук",
    "stock": 10
  },
  {
    "id": 2,
    "name": "Миша",
    "price": 500,
    "description": "Бездротова миша",
    "stock": 50
  },
  {
    "id": 3,
    "name": "Клавіатура",
    "price": 1200,
    "description": "Механічна клавіатура",
    "stock": 30
  }
]
```

---

### 2. Отримати продукт за ID
```powershell
# PowerShell
Invoke-RestMethod -Uri "http://localhost:5001/api/products/1" -Method Get | ConvertTo-Json

# curl
curl http://localhost:5001/api/products/1
```

---

### 3. Створити новий продукт
```powershell
# PowerShell
$body = @{
    name = "Веб-камера"
    price = 1500
    description = "HD веб-камера"
    stock = 20
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5001/api/products" `
    -Method Post `
    -Body $body `
    -ContentType "application/json" | ConvertTo-Json

# curl
curl -X POST http://localhost:5001/api/products `
  -H "Content-Type: application/json" `
  -d '{"name":"Веб-камера","price":1500,"description":"HD веб-камера","stock":20}'
```

---

### 4. Оновити продукт
```powershell
# PowerShell
$body = @{
    name = "Ноутбук Gaming"
    price = 35000
    description = "Потужний ігровий ноутбук"
    stock = 5
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5001/api/products/1" `
    -Method Put `
    -Body $body `
    -ContentType "application/json" | ConvertTo-Json

# curl
curl -X PUT http://localhost:5001/api/products/1 `
  -H "Content-Type: application/json" `
  -d '{"name":"Ноутбук Gaming","price":35000,"description":"Потужний ігровий ноутбук","stock":5}'
```

---

### 5. Видалити продукт
```powershell
# PowerShell
Invoke-RestMethod -Uri "http://localhost:5001/api/products/1" -Method Delete

# curl
curl -X DELETE http://localhost:5001/api/products/1
```

---

### 6. Перевірити доступність (тільки для 3-Layer)
```powershell
# PowerShell
Invoke-RestMethod -Uri "http://localhost:5001/api/products/1/check-availability?quantity=5" `
    -Method Get | ConvertTo-Json

# curl
curl "http://localhost:5001/api/products/1/check-availability?quantity=5"
```

**Відповідь:**
```json
{
  "productId": 1,
  "quantity": 5,
  "isAvailable": true
}
```

---

### 7. Зменшити запаси (тільки для Onion Architecture)
```powershell
# PowerShell
$body = @{ quantity = 3 } | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5003/api/products/1/reduce-stock" `
    -Method Post `
    -Body $body `
    -ContentType "application/json" | ConvertTo-Json

# curl
curl -X POST http://localhost:5003/api/products/1/reduce-stock `
  -H "Content-Type: application/json" `
  -d '{"quantity":3}'
```

---

## 🧪 Тестування валідації

### Спробуйте створити продукт з невалідними даними:

#### Від'ємна ціна
```powershell
$body = @{
    name = "Тестовий продукт"
    price = -100
    description = "Це не спрацює"
    stock = 10
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5001/api/products" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"
```

**Очікувана помилка:** `400 Bad Request - "Ціна має бути більше 0"`

---

#### Порожня назва
```powershell
$body = @{
    name = ""
    price = 100
    description = "Це не спрацює"
    stock = 10
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5001/api/products" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"
```

**Очікувана помилка:** `400 Bad Request - "Назва продукту не може бути порожньою"`

---

#### Від'ємна кількість
```powershell
$body = @{
    name = "Тест"
    price = 100
    description = "Це не спрацює"
    stock = -5
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5001/api/products" `
    -Method Post `
    -Body $body `
    -ContentType "application/json"
```

**Очікувана помилка:** `400 Bad Request - "Кількість не може бути від'ємною"`

---

## 📊 Порівняти поведінку архітектур

### Тест 1: Створити продукт у всіх прикладах
```powershell
$testProduct = @{
    name = "Тестовий продукт"
    price = 999
    description = "Для порівняння"
    stock = 100
} | ConvertTo-Json

# 3-Layer
Invoke-RestMethod -Uri "http://localhost:5001/api/products" `
    -Method Post -Body $testProduct -ContentType "application/json"

# Clean
Invoke-RestMethod -Uri "http://localhost:5002/api/products" `
    -Method Post -Body $testProduct -ContentType "application/json"

# Onion
Invoke-RestMethod -Uri "http://localhost:5003/api/products" `
    -Method Post -Body $testProduct -ContentType "application/json"

# Hexagonal
Invoke-RestMethod -Uri "http://localhost:5004/api/products" `
    -Method Post -Body $testProduct -ContentType "application/json"

# Vertical Slice
Invoke-RestMethod -Uri "http://localhost:5005/api/products" `
    -Method Post -Body $testProduct -ContentType "application/json"
```

### Тест 2: Порівняти швидкість відповіді
```powershell
# Перевірити час відповіді кожного API
Measure-Command {
    Invoke-RestMethod -Uri "http://localhost:5001/api/products" -Method Get
} | Select-Object TotalMilliseconds

Measure-Command {
    Invoke-RestMethod -Uri "http://localhost:5002/api/products" -Method Get
} | Select-Object TotalMilliseconds

Measure-Command {
    Invoke-RestMethod -Uri "http://localhost:5003/api/products" -Method Get
} | Select-Object TotalMilliseconds

Measure-Command {
    Invoke-RestMethod -Uri "http://localhost:5004/api/products" -Method Get
} | Select-Object TotalMilliseconds

Measure-Command {
    Invoke-RestMethod -Uri "http://localhost:5005/api/products" -Method Get
} | Select-Object TotalMilliseconds
```

---

## 🎯 Завдання для самостійного дослідження

### Рівень 1 - Початковий
1. ✅ Запустіть кожен проект
2. ✅ Відкрийте Swagger UI для кожного
3. ✅ Створіть новий продукт через Swagger
4. ✅ Спробуйте створити продукт з невалідними даними

### Рівень 2 - Середній
5. ✅ Порівняйте структуру папок у кожному проекті
6. ✅ Знайдіть де знаходиться валідація в кожній архітектурі
7. ✅ Подивіться на залежності між проектами (`.csproj` файли)
8. ✅ Знайдіть інтерфейс `IProductRepository` в кожній архітектурі

### Рівень 3 - Просунутий
9. ✅ Додайте новий метод до Product entity (наприклад, `ApplyDiscount`)
10. ✅ Додайте нову властивість до Product (наприклад, `Category`)
11. ✅ Створіть новий endpoint `GET /api/products/low-stock`
12. ✅ Порівняйте скільки файлів треба змінити для кожної зміни

---

## 🐛 Troubleshooting

### Помилка: "Address already in use"
```powershell
# Знайти процес на порту
netstat -ano | findstr :5001

# Завершити процес (замініть PID на ваш)
taskkill /PID <номер_процесу> /F
```

### Помилка: "dotnet command not found"
```powershell
# Перевірити чи встановлено .NET
dotnet --version

# Якщо немає, завантажте з https://dotnet.microsoft.com/download
```

### Помилка при збірці проекту
```powershell
# Очистити та перебудувати
dotnet clean
dotnet restore
dotnet build
```

---

## 📚 Що вивчати код

### Для 3-Layer Architecture
1. Подивіться [Program.cs](./3-Layer-Architecture/Program.cs) - реєстрація сервісів
2. Перейдіть до [ProductsController](./3-Layer-Architecture/Controllers/ProductsController.cs)
3. Подивіться як контролер викликає [ProductService](./3-Layer-Architecture/BusinessLogicLayer/ProductService.cs)
4. Подивіться як сервіс викликає [ProductRepository](./3-Layer-Architecture/DataAccessLayer/ProductRepository.cs)

### Для Clean Architecture
1. Почніть з [Product Entity](./Clean-Architecture/CleanArchitecture.Domain/Entities/Product.cs) (Domain - центр!)
2. Подивіться [Commands і Queries](./Clean-Architecture/CleanArchitecture.Application/Products/)
3. Зверніть увагу на [IProductRepository](./Clean-Architecture/CleanArchitecture.Application/Interfaces/IProductRepository.cs) в Application
4. Побачте реалізацію в [Infrastructure](./Clean-Architecture/CleanArchitecture.Infrastructure/Persistence/ProductRepository.cs)

### Для Onion Architecture
1. Почніть з ядра - [Product Entity](./Onion-Architecture/OnionArchitecture.Domain/Entities/Product.cs)
2. Перейдіть до [ProductDomainService](./Onion-Architecture/OnionArchitecture.Domain/Services/ProductDomainService.cs)
3. Побачте координацію в [ProductService](./Onion-Architecture/OnionArchitecture.Application/Services/ProductService.cs)
4. Зверніть увагу на розташування [IProductRepository](./Onion-Architecture/OnionArchitecture.Domain/Interfaces/IProductRepository.cs) в Domain

---

## ✨ Корисні команди

```powershell
# Подивитися структуру проекту
tree /F

# Порахувати рядки коду
Get-ChildItem -Recurse -Include *.cs | Get-Content | Measure-Object -Line

# Знайти всі використання Product
Get-ChildItem -Recurse -Include *.cs | Select-String "Product" | Group-Object Path

# Побудувати всі проекти
dotnet build "c:\Users\VladyslavMakukh\Desktop\Junk\Architectures\3-Layer-Architecture"
dotnet build "c:\Users\VladyslavMakukh\Desktop\Junk\Architectures\Clean-Architecture\CleanArchitecture.API"
dotnet build "c:\Users\VladyslavMakukh\Desktop\Junk\Architectures\Onion-Architecture\OnionArchitecture.API"
```

Успіхів у вивченні архітектур! 🚀
