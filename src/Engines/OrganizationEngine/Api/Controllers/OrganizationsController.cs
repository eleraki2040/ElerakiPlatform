using Eleraki.OrganizationEngine.Application.Commands;
using Eleraki.OrganizationEngine.Application.DTOs;
using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.OrganizationEngine.Api.Controllers;

/// <summary>
/// Provides endpoints for managing organizations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganizationsController"/> class.
    /// </summary>
    /// <param name="mediator">The MediatR mediator.</param>
    public OrganizationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new organization.
    /// </summary>
    /// <param name="command">The command containing organization creation data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created organization location.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        if (!result.IsSuccess)
            return BadRequest(result.Error);
        
        return CreatedAtAction(nameof(GetOrganizationById), new { id = result.Value }, null);
    }

    /// <summary>
    /// Gets an organization by its identifier.
    /// </summary>
    /// <param name="id">The organization identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The organization details.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrganizationById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetOrganizationByIdQuery(id), cancellationToken);
        
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }
}