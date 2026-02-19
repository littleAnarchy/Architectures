using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Products.Commands;

/// <summary>
/// APPLICATION LAYER - Use Case для оновлення продукту
/// ПРИМІТКА: Це CQS Command. Альтернатива - метод UpdateAsync() у сервісі.
/// </summary>
public record UpdateProductCommand(
    int Id,
    string Name,
    decimal Price,
    string Description,
    int Stock
) : IRequest<Product>;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
{
    private readonly IProductRepository _repository;

    public UpdateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null)
            throw new KeyNotFoundException($"Продукт з ID {request.Id} не знайдено");

        // Оновлюємо через методи сутності, які містять валідацію
        product.SetName(request.Name);
        product.SetPrice(request.Price);
        product.Description = request.Description;
        product.SetStock(request.Stock);

        return await _repository.UpdateAsync(product, cancellationToken);
    }
}
