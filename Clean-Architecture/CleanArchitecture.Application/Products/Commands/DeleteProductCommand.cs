using CleanArchitecture.Application.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Products.Commands;

/// <summary>
/// APPLICATION LAYER - Use Case для видалення продукту
/// ПРИМІТКА: Це CQS Command. Альтернатива - метод DeleteAsync() у сервісі.
/// </summary>
public record DeleteProductCommand(int Id) : IRequest<bool>;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repository;

    public DeleteProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken) =>
        _repository.DeleteAsync(request.Id, cancellationToken);
}
