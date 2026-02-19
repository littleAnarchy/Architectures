using MediatR;
using VerticalSlice.Domain;
using VerticalSlice.Infrastructure;

namespace VerticalSlice.Features.Products.Create;

public record CreateProductCommand(string Name, decimal Price, string Description, int Stock) : IRequest<Product>;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _repo;
    public CreateProductHandler(IProductRepository repo) => _repo = repo;

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product { Name = request.Name, Price = request.Price, Description = request.Description, Stock = request.Stock };
        return await _repo.AddAsync(product, cancellationToken);
    }
}
