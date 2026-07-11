using Eleraki.IdentityEngine.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eleraki.IdentityEngine.Api.Controllers;

/// <summary>
/// Users endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersController"/> class.
    /// </summary>
    /// <param name="sender">The sender.</param>
    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="request">The create user request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created user ID.</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(request.Name, request.Email, request.Password);

        var userId = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(Create), new { id = userId }, userId);
    }
}

/// <summary>
/// Request DTO for creating a User.
/// </summary>
public sealed record CreateUserRequest(string Name, string Email, string Password);
