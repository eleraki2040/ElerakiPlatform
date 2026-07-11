using Eleraki.Enterprise.Application.Commands;
using Eleraki.Enterprise.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.Enterprise.Api.Controllers;

/// <summary>
/// Enterprises endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EnterprisesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnterprisesController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    public EnterprisesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new enterprise.
    /// </summary>
    /// <param name="command">The create enterprise command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created enterprise ID.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateEnterprise([FromBody] CreateEnterpriseCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        
        return CreatedAtAction(nameof(GetEnterpriseById), new { id = result.Value }, result.Value);
    }

    /// <summary>
    /// Gets an enterprise by ID.
    /// </summary>
    /// <param name="id">The enterprise ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The enterprise.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEnterpriseById(Guid id, CancellationToken cancellationToken)
    {
        var enterprise = await _mediator.Send(new GetEnterpriseByIdQuery(id), cancellationToken);
        
        if (enterprise is null)
            return NotFound();
        
        return Ok(enterprise);
    }
}
