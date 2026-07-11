using Eleraki.AuthorizationEngine.Domain;
using Eleraki.AuthorizationEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.AuthorizationEngine.Application.Permissions.Commands;

public sealed record CreatePermissionCommand(string Name, string Code, string? Description, string Resource, string Type) : IRequest<Guid>;

public sealed class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Guid>
{
    private readonly IAuthorizationRepository _repository;

    public CreatePermissionCommandHandler(IAuthorizationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var type = PermissionType.Create(request.Type);
        var permission = Permission.Create(request.Name, request.Code, request.Description, request.Resource, type);

        await _repository.AddPermissionAsync(permission, cancellationToken);

        return permission.Id.Value;
    }
}
