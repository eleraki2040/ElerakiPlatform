using Eleraki.PurchasingEngine.Application.Commands;
using Eleraki.PurchasingEngine.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.PurchasingEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseOrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public PurchaseOrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePurchaseOrder([FromBody] CreatePurchaseOrderCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetPurchaseOrderById), new { id = result.Value }, null);
    }

    [HttpPost("{id:guid}/lines")]
    public async Task<IActionResult> AddPurchaseOrderLine(Guid id, [FromBody] AddPurchaseOrderLineCommand command, CancellationToken cancellationToken)
    {
        if (id != command.PurchaseOrderId)
            return BadRequest("ID mismatch.");

        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPost("{id:guid}/approve")]
    public async Task<IActionResult> ApprovePurchaseOrder(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ApprovePurchaseOrderCommand(id), cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPost("{id:guid}/receive")]
    public async Task<IActionResult> ReceivePurchaseOrder(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ReceivePurchaseOrderCommand(id), cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPurchaseOrderById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPurchaseOrderByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }
}
