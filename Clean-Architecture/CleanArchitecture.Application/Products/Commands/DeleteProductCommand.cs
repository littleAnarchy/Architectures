using CleanArchitecture.Application.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Products.Commands;

/// <summary>
/// APPLICATION LAYER - Use Case для видалення продукту
/// </summary>
public record DeleteProductCommand(int Id) : IRequest<bool>;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repository;

    public DeleteProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.Id);
    }
}
