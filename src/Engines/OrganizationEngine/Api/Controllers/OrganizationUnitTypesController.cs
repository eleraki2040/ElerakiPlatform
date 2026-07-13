using Eleraki.OrganizationEngine.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.OrganizationEngine.Api.Controllers;

/// <summary>
/// Provides endpoints for managing organization unit types.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrganizationUnitTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationUnitTypesController"/> class.
    /// </summary>
    /// <param name="mediator">The MediatR mediator.</param>
    public OrganizationUnitTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new organization unit type.
    /// </summary>
    /// <param name="command">The command containing organization unit type creation data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created organization unit type ID.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrganizationUnitType([FromBody] CreateOrganizationUnitTypeCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        
        return CreatedAtAction(nameof(GetOrganizationUnitTypeById), new { id = result.Value }, null);
    }

    /// <summary>
    /// Gets an organization unit type by its identifier.
    /// </summary>
    /// <param name="id">The organization unit type identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The organization unit type details.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrganizationUnitTypeById(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Implement GetOrganizationUnitTypeByIdQuery
        return await Task.FromResult<IActionResult>(NotFound());
    }
}
