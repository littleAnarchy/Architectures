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
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        try
        {
            var created = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> Update(int id, [FromBody] Product product)
    {
        product.Id = id;
        try
        {
            var updated = await _productService.UpdateProductAsync(product);
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
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("{id}/check-availability")]
    public async Task<ActionResult<bool>> CheckAvailability(int id, [FromQuery] int quantity)
    {
        var isAvailable = await _productService.CheckAvailabilityAsync(id, quantity);
        return Ok(new { productId = id, quantity, isAvailable });
    }
}
