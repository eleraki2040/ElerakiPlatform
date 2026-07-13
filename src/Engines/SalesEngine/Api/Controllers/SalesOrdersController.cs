using Eleraki.SalesEngine.Application.Commands;
using Eleraki.SalesEngine.Application.DTOs;
using Eleraki.SalesEngine.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.SalesEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesOrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalesOrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSalesOrder([FromBody] CreateSalesOrderCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetSalesOrderById), new { id = result.Value }, null);
    }

    [HttpPost("lines")]
    public async Task<IActionResult> AddSalesOrderLine([FromBody] AddSalesOrderLineCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetSalesOrderById), new { id = command.SalesOrderId }, null);
    }

    [HttpPost("{id:guid}/approve")]
    public async Task<IActionResult> ApproveSalesOrder(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ApproveSalesOrderCommand(id), cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpPost("{id:guid}/fulfill")]
    public async Task<IActionResult> FulfillSalesOrder(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new FulfillSalesOrderCommand(id), cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSalesOrderById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetSalesOrderByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }
}
