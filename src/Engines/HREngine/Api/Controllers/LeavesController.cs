using Eleraki.HREngine.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.HREngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeavesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeavesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestLeave([FromBody] RequestLeaveCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(new { id = result.Value });
    }

    [HttpPost("{id:guid}/approve")]
    public async Task<IActionResult> ApproveLeave(Guid id, [FromBody] ApproveLeaveCommand command, CancellationToken cancellationToken)
    {
        var updatedCommand = command with { LeaveId = id };
        var result = await _mediator.Send(updatedCommand, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok();
    }
}
