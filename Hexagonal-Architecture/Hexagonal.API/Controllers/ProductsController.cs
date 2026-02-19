using Microsoft.AspNetCore.Mvc;
using HexagonalArchitecture.Ports;
using HexagonalArchitecture.Domain;

namespace HexagonalArchitecture.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;

    public ProductsController(IProductRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var products = await _repo.GetAllAsync(cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var p = await _repo.GetByIdAsync(id, cancellationToken);
        if (p == null) return NotFound();
        return Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product, CancellationToken cancellationToken)
    {
        var created = await _repo.AddAsync(product, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product product, CancellationToken cancellationToken)
    {
        product.Id = id;
        var updated = await _repo.UpdateAsync(product, cancellationToken);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var ok = await _repo.DeleteAsync(id, cancellationToken);
        if (!ok) return NotFound();
        return NoContent();
    }
}
