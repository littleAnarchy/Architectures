using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Products.Queries;

/// <summary>
/// APPLICATION LAYER - Use Case для отримання продукту за ID
/// 
/// ПРИМІТКА: Це CQS Query (читання без зміни стану).
/// Альтернатива - метод GetByIdAsync() у сервісі.
/// </summary>
public record GetProductByIdQuery(int Id) : IRequest<Product?>;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductRepository _repository;

    public GetProductByIdHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
        _repository.GetByIdAsync(request.Id, cancellationToken);
}
