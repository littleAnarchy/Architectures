using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Products.Queries;

/// <summary>
/// APPLICATION LAYER - Use Case для отримання всіх продуктів
/// Кожен Use Case - окремий клас з конкретною бізнес-операцією
/// 
/// ПРИМІТКА: CQS (Command/Query Separation) + MediatR - це опціональний підхід.
/// Альтернатива: звичайні сервіси з методами GetAllAsync(), CreateAsync() тощо.
/// Головне в Clean Architecture - не CQS, а правильність залежностей (Dependency Rule).
/// 
/// Це CQS, а не повноцінний CQRS (бази не розділені, один репозиторій).
/// </summary>
public record GetAllProductsQuery : IRequest<IEnumerable<Product>>;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _repository;

    public GetAllProductsHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
