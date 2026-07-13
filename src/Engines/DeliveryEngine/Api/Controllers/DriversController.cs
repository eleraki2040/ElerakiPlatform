using Eleraki.DeliveryEngine.Application.Commands;
using Eleraki.DeliveryEngine.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.DeliveryEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{
    private readonly IMediator _mediator;

    public DriversController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDriverCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(result.Error);
        return Ok(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDriverByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }
}
