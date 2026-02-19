using Microsoft.EntityFrameworkCore;
using VerticalSlice.Domain;

namespace VerticalSlice.Infrastructure;

public class EfCoreProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public EfCoreProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }
}
