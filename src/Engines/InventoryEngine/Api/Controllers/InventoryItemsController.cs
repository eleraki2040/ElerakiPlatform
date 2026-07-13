using Eleraki.InventoryEngine.Application.Commands;
using Eleraki.InventoryEngine.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.InventoryEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateInventoryItem([FromBody] CreateInventoryItemCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetInventoryItemById), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetInventoryItemById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetInventoryItemByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpPut("{id:guid}/stock")]
    public async Task<IActionResult> UpdateStock(Guid id, [FromBody] UpdateStockCommand command, CancellationToken cancellationToken)
    {
        if (id != command.InventoryItemId)
            return BadRequest("ID mismatch.");

        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPost("{id:guid}/transfer")]
    public async Task<IActionResult> TransferStock(Guid id, [FromBody] TransferStockCommand command, CancellationToken cancellationToken)
    {
        if (id != command.InventoryItemId)
            return BadRequest("ID mismatch.");

        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return NoContent();
    }
}
