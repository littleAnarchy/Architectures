using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSlice.Features.Products.Create;
using VerticalSlice.Features.Products.GetAll;
using VerticalSlice.Features.Products.GetById;

namespace VerticalSlice.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllProductsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand cmd)
    {
        var created = await _mediator.Send(cmd);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }
}
