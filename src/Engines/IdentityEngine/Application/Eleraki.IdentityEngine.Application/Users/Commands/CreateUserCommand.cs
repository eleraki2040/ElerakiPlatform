using Eleraki.IdentityEngine.Domain;
using MediatR;

namespace Eleraki.IdentityEngine.Application.Users.Commands;

/// <summary>
/// Command to create a new user.
/// </summary>
/// <param name="Name">The user name.</param>
/// <param name="Email">The user email.</param>
/// <param name="Password">The user password.</param>
public sealed record CreateUserCommand(string Name, string Email, string Password) : IRequest<Guid>;

/// <summary>
/// Handler for CreateUserCommand.
/// </summary>
public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly Eleraki.IdentityEngine.Domain.Repositories.IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    public CreateUserCommandHandler(Eleraki.IdentityEngine.Domain.Repositories.IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc/>
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var name = UserName.Create(request.Name);
        var email = global::Eleraki.SharedKernel.ValueObjects.Email.Create(request.Email);
        var password = UserPassword.Create(request.Password);

        var user = User.Create(name, email, password);

        await _userRepository.AddAsync(user, cancellationToken);

        return user.Id.Value;
    }
}
