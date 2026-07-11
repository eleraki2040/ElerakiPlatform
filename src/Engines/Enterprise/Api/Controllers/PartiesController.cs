using Eleraki.Enterprise.Application.Parties.Commands;
using Eleraki.Enterprise.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.Enterprise.Api.Controllers;

/// <summary>
/// Parties endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PartiesController : ControllerBase
{
    private readonly ISender _sender;

    /// <summary>
    /// Initializes a new instance of the <see cref="PartiesController"/> class.
    /// </summary>
    /// <param name="sender">The sender.</param>
    public PartiesController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Creates a new Party.
    /// </summary>
    /// <param name="request">The create party request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created party ID.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreatePartyRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePartyCommand(request.Name, request.Type);

        var partyId = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Create), new { id = partyId }, partyId);
    }
}

/// <summary>
/// Request DTO for creating a Party.
/// </summary>
public sealed record CreatePartyRequest(string Name, PartyType Type);
