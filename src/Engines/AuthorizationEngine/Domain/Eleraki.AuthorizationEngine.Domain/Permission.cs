using Eleraki.AuthorizationEngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.AuthorizationEngine.Domain;

public sealed class Permission : AggregateRoot<PermissionId>
{
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string Resource { get; private set; } = string.Empty;
    public PermissionType Type { get; private set; } = null!;
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private Permission(PermissionId id) : base(id) { }

    public static Permission Create(string name, string code, string? description, string resource, PermissionType type)
    {
        var permission = new Permission(PermissionId.New())
        {
            Name = name,
            Code = code,
            Description = description,
            Resource = resource,
            Type = type,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        permission.RaiseDomainEvent(new PermissionCreatedDomainEvent(permission.Id));
        return permission;
    }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PermissionUpdatedDomainEvent(Id));
    }

    public void Delete()
    {
        RaiseDomainEvent(new PermissionDeletedDomainEvent(Id));
    }
}
