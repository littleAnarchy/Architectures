using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.Interfaces;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.API.Controllers;

/// <summary>
/// OUTER LAYER - Контролер (зовнішній шар цибулі)
/// Взаємодіє тільки з Application Services
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
        try
        {
            var product = await _productService.GetProductByIdAsync(id, cancellationToken);
            return Ok(product);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create([FromBody] CreateProductDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productService.CreateProductAsync(dto.Name, dto.Price, dto.Description, dto.Stock, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> Update(int id, [FromBody] UpdateProductDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productService.UpdateProductAsync(id, dto.Name, dto.Price, dto.Description, dto.Stock, cancellationToken);
            return Ok(product);
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

    [HttpPost("{id}/reduce-stock")]
    public async Task<ActionResult> ReduceStock(int id, [FromBody] ReduceStockDto dto, CancellationToken cancellationToken)
    {
        try
        {
            await _productService.ReduceStockAsync(id, dto.Quantity, cancellationToken);
            return Ok(new { message = "Запаси успішно зменшено" });
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

// DTOs для API
public record CreateProductDto(string Name, decimal Price, string Description, int Stock);
public record UpdateProductDto(string Name, decimal Price, string Description, int Stock);
public record ReduceStockDto(int Quantity);
