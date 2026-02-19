using MediatR;
using VerticalSlice.Domain;
using VerticalSlice.Infrastructure;

namespace VerticalSlice.Features.Products.GetById;

public record GetProductByIdQuery(int Id) : IRequest<Product?>;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
{
    private readonly IProductRepository _repo;
    public GetProductByIdHandler(IProductRepository repo) => _repo = repo;
    
    public Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
        _repo.GetByIdAsync(request.Id, cancellationToken);
}
