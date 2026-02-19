using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Products.Commands;

/// <summary>
/// APPLICATION LAYER - Use Case для створення продукту
/// 
/// 🎯 Цей Command = Use Case з Clean Architecture.
/// Команди - це Use Cases що змінюють стан системи.
/// 
/// ПРИМІТКА: Це CQS Command (зміна стану без повернення даних, окрім результату).
/// Альтернатива - Use Case через метод CreateAsync() у сервісі.
/// CQS не є обов'язковим для Clean Architecture.
/// </summary>
public record CreateProductCommand(
    string Name,
    decimal Price,
    string Description,
    int Stock
) : IRequest<Product>;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _repository;

    public CreateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Створюємо сутність через конструктор, який валідує дані
        var product = new Product(
            request.Name,
            request.Price,
            request.Description,
            request.Stock
        );

        return await _repository.AddAsync(product);
    }
}
