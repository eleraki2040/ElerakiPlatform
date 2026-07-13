using Eleraki.HREngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.HREngine.Domain;

public sealed class Position : AggregateRoot<PositionId>
{
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string DepartmentId { get; private set; } = string.Empty;
    public PositionStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Position(PositionId id) : base(id)
    {
    }

    public static Position Create(string title, string departmentId, string? description = null)
    {
        Guard.NotNullOrEmpty(title, nameof(title));
        Guard.NotNullOrEmpty(departmentId, nameof(departmentId));

        var position = new Position(PositionId.New())
        {
            Title = title.Trim(),
            Description = description?.Trim(),
            DepartmentId = departmentId,
            Status = PositionStatus.Active,
            CreatedAt = Clock.UtcNow,
            UpdatedAt = Clock.UtcNow
        };

        position.RaiseDomainEvent(new PositionCreatedDomainEvent(position.Id, Guid.NewGuid(), Clock.UtcNow));

        return position;
    }

    public void Update(string title, string? description = null)
    {
        Guard.NotNullOrEmpty(title, nameof(title));

        Title = title.Trim();
        Description = description?.Trim();
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new PositionUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Activate()
    {
        if (Status == PositionStatus.Active)
            return;

        Status = PositionStatus.Active;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new PositionActivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Deactivate()
    {
        if (Status == PositionStatus.Inactive)
            return;

        Status = PositionStatus.Inactive;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new PositionDeactivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

public enum PositionStatus
{
    Active = 1,
    Inactive = 2
}
