using MediatR;
using VerticalSlice.Domain;
using VerticalSlice.Infrastructure;

namespace VerticalSlice.Features.Products.GetAll;

public record GetAllProductsQuery : IRequest<IEnumerable<Product>>;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _repo;
    public GetAllProductsHandler(IProductRepository repo) => _repo = repo;
    public Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return _repo.GetAllAsync();
    }
}
