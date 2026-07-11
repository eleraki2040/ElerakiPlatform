using Eleraki.AuthorizationEngine.Application.Roles.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.AuthorizationEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly ISender _sender;

    public RolesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateRoleCommand(request.Name, request.Description);
        var id = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id }, id);
    }
}

public sealed record CreateRoleRequest(string Name, string? Description);
