using Eleraki.OrganizationEngine.Domain.OrganizationUnits;
using Eleraki.OrganizationEngine.Domain.Repositories;
using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.OrganizationEngine.Application.Commands;

/// <summary>
/// Command to create a new organization unit.
/// </summary>
/// <param name="OrganizationId">The organization ID.</param>
/// <param name="Name">The organization unit name.</param>
/// <param name="Code">The organization unit code.</param>
/// <param name="OrganizationUnitTypeId">The organization unit type ID.</param>
/// <param name="ParentId">Optional parent organization unit ID.</param>
/// <param name="Description">Optional description.</param>
public record CreateOrganizationUnitCommand(
    Guid OrganizationId,
    string Name,
    string Code,
    Guid OrganizationUnitTypeId,
    Guid? ParentId = null,
    string? Description = null) : IRequest<Result<Guid>>;

/// <summary>
/// Handles the CreateOrganizationUnitCommand.
/// </summary>
public class CreateOrganizationUnitCommandHandler : IRequestHandler<CreateOrganizationUnitCommand, Result<Guid>>
{
    private readonly IOrganizationUnitRepository _organizationUnitRepository;
    private readonly IOrganizationRepository _organizationRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateOrganizationUnitCommandHandler"/> class.
    /// </summary>
    /// <param name="organizationUnitRepository">The organization unit repository.</param>
    /// <param name="organizationRepository">The organization repository.</param>
    public CreateOrganizationUnitCommandHandler(
        IOrganizationUnitRepository organizationUnitRepository,
        IOrganizationRepository organizationRepository)
    {
        _organizationUnitRepository = organizationUnitRepository;
        _organizationRepository = organizationRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<Guid>> Handle(CreateOrganizationUnitCommand request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(OrganizationId.From(request.OrganizationId), cancellationToken);
        if (organization is null)
            return Result<Guid>.Failure(Error.NotFound("Organization not found."));

        var existing = await _organizationUnitRepository.ExistsByCodeAsync(
            OrganizationId.From(request.OrganizationId),
            request.Code,
            cancellationToken);
        if (existing)
            return Result<Guid>.Failure(Error.Conflict("Organization unit code already exists."));

        var organizationUnit = OrganizationUnit.Create(
            OrganizationId.From(request.OrganizationId),
            request.Name,
            request.Code,
            OrganizationUnitTypeId.From(request.OrganizationUnitTypeId),
            request.ParentId.HasValue ? OrganizationUnitId.From(request.ParentId.Value) : null,
            request.Description);

        await _organizationUnitRepository.AddAsync(organizationUnit, cancellationToken);

        return Result<Guid>.Success(organizationUnit.Id.Value);
    }
}
