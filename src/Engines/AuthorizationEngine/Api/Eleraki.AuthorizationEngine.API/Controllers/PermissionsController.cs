using Eleraki.AuthorizationEngine.Application.Permissions.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.AuthorizationEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly ISender _sender;

    public PermissionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreatePermissionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreatePermissionCommand(request.Name, request.Code, request.Description, request.Resource, request.Type);
        var id = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id }, id);
    }
}

public sealed record CreatePermissionRequest(string Name, string Code, string? Description, string Resource, string Type);
