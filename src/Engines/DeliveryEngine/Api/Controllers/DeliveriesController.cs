using Eleraki.DeliveryEngine.Application.Commands;
using Eleraki.DeliveryEngine.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.DeliveryEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeliveriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDelivery([FromBody] CreateDeliveryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDeliveryByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpGet("tracking/{trackingNumber}")]
    public async Task<IActionResult> GetByTrackingNumber(string trackingNumber, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDeliveryByTrackingNumberQuery(trackingNumber), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpPost("{id:guid}/assign-driver")]
    public async Task<IActionResult> AssignDriver(Guid id, [FromBody] AssignDriverCommand command, CancellationToken cancellationToken)
    {
        if (id != command.DeliveryId)
            return BadRequest("Delivery ID mismatch.");

        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok();
    }

    [HttpPost("{id:guid}/start")]
    public async Task<IActionResult> Start(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new StartDeliveryCommand(id), cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok();
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CompleteDeliveryCommand(id), cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok();
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, [FromBody] CancelDeliveryCommand command, CancellationToken cancellationToken)
    {
        if (id != command.DeliveryId)
            return BadRequest("Delivery ID mismatch.");

        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok();
    }
}
