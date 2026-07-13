using Eleraki.OrganizationEngine.Application.Commands;
using Eleraki.OrganizationEngine.Application.DTOs;
using Eleraki.OrganizationEngine.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.OrganizationEngine.Api.Controllers;

/// <summary>
/// Provides endpoints for managing organization units.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrganizationUnitsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationUnitsController"/> class.
    /// </summary>
    /// <param name="mediator">The MediatR mediator.</param>
    public OrganizationUnitsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new organization unit.
    /// </summary>
    /// <param name="command">The command containing organization unit creation data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created organization unit ID.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrganizationUnit([FromBody] CreateOrganizationUnitCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        
        return CreatedAtAction(nameof(GetOrganizationUnitById), new { id = result.Value }, null);
    }

    /// <summary>
    /// Gets an organization unit by its identifier.
    /// </summary>
    /// <param name="id">The organization unit identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The organization unit details.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrganizationUnitById(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Implement GetOrganizationUnitByIdQuery
        return await Task.FromResult<IActionResult>(NotFound());
    }
}
