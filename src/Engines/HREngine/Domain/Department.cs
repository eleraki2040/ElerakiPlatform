using Eleraki.HREngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.HREngine.Domain;

public sealed class Department : AggregateRoot<DepartmentId>
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DepartmentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Department(DepartmentId id) : base(id)
    {
    }

    public static Department Create(string name, string? description = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));

        var department = new Department(DepartmentId.New())
        {
            Name = name.Trim(),
            Description = description?.Trim(),
            Status = DepartmentStatus.Active,
            CreatedAt = Clock.UtcNow,
            UpdatedAt = Clock.UtcNow
        };

        department.RaiseDomainEvent(new DepartmentCreatedDomainEvent(department.Id, Guid.NewGuid(), Clock.UtcNow));

        return department;
    }

    public void Update(string name, string? description = null)
    {
        Guard.NotNullOrEmpty(name, nameof(name));

        Name = name.Trim();
        Description = description?.Trim();
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new DepartmentUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Activate()
    {
        if (Status == DepartmentStatus.Active)
            return;

        Status = DepartmentStatus.Active;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new DepartmentActivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Deactivate()
    {
        if (Status == DepartmentStatus.Inactive)
            return;

        Status = DepartmentStatus.Inactive;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new DepartmentDeactivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

public enum DepartmentStatus
{
    Active = 1,
    Inactive = 2
}
