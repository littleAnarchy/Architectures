using Microsoft.AspNetCore.Mvc;
using ThreeLayerArchitecture.BusinessLogicLayer;
using ThreeLayerArchitecture.Models;

namespace ThreeLayerArchitecture.Controllers;

/// <summary>
/// PRESENTATION LAYER - Контролер для роботи з продуктами
/// Відповідає за обробку HTTP запитів та відповідей
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _productService.GetAllProductsAsync(cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductByIdAsync(id, cancellationToken);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] Product product, CancellationToken cancellationToken)
    {
        try
        {
            var created = await _productService.CreateProductAsync(product, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> Update(int id, [FromBody] Product product, CancellationToken cancellationToken)
    {
        product.Id = id;
        try
        {
            var updated = await _productService.UpdateProductAsync(product, cancellationToken);
            return Ok(updated);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _productService.DeleteProductAsync(id, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("{id}/check-availability")]
    public async Task<ActionResult<bool>> CheckAvailability(int id, [FromQuery] int quantity, CancellationToken cancellationToken)
    {
        var isAvailable = await _productService.CheckAvailabilityAsync(id, quantity, cancellationToken);
        return Ok(new { productId = id, quantity, isAvailable });
    }
}
