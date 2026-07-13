using Eleraki.HREngine.Application.Commands;
using Eleraki.HREngine.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.HREngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePosition([FromBody] CreatePositionCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return CreatedAtAction(nameof(GetPositionById), new { id = result.Value }, result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPositionById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPositionByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }
}
