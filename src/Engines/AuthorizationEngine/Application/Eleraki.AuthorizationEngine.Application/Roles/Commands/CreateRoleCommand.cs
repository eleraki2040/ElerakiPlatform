using Eleraki.AuthorizationEngine.Domain;
using Eleraki.AuthorizationEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.AuthorizationEngine.Application.Roles.Commands;

public sealed record CreateRoleCommand(string Name, string? Description) : IRequest<Guid>;

public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Guid>
{
    private readonly IAuthorizationRepository _repository;

    public CreateRoleCommandHandler(IAuthorizationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = Role.Create(request.Name, request.Description);

        await _repository.AddRoleAsync(role, cancellationToken);

        return role.Id.Value;
    }
}
