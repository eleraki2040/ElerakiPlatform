using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.SharedKernel.Entities;

/// <summary>
/// Base class for auditable entities.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public abstract class AuditableEntity<TId> : Entity<TId>
    where TId : IStronglyTypedId
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuditableEntity{TId}"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected AuditableEntity(TId id) : base(id)
    {
    }
    /// <summary>
    /// Gets the user who created the entity.
    /// </summary>
    public string? CreatedBy { get; private set; }

    /// <summary>
    /// Gets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the user who last modified the entity.
    /// </summary>
    public string? ModifiedBy { get; private set; }

    /// <summary>
    /// Gets the date and time when the entity was last modified.
    /// </summary>
    public DateTime? ModifiedAt { get; private set; }

    /// <summary>
    /// Sets the audit fields for entity creation.
    /// </summary>
    /// <param name="createdBy">The user who created the entity.</param>
    public void SetCreatedAudit(string createdBy)
    {
        CreatedBy = createdBy;
        CreatedAt = Clock.UtcNow;
    }

    /// <summary>
    /// Sets the audit fields for entity modification.
    /// </summary>
    /// <param name="modifiedBy">The user who modified the entity.</param>
    public void SetModifiedAudit(string modifiedBy)
    {
        ModifiedBy = modifiedBy;
        ModifiedAt = Clock.UtcNow;
    }
}
