using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.OrganizationUnitTypes;
using Eleraki.OrganizationEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.OrganizationEngine.Application.Commands;

/// <summary>
/// Command to create a new organization unit type.
/// </summary>
/// <param name="Name">The organization unit type name.</param>
/// <param name="Description">Optional description.</param>
public record CreateOrganizationUnitTypeCommand(string Name, string? Description = null) : IRequest<Result<Guid>>;

/// <summary>
/// Handles the CreateOrganizationUnitTypeCommand.
/// </summary>
public class CreateOrganizationUnitTypeCommandHandler : IRequestHandler<CreateOrganizationUnitTypeCommand, Result<Guid>>
{
    private readonly IOrganizationUnitTypeRepository _organizationUnitTypeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateOrganizationUnitTypeCommandHandler"/> class.
    /// </summary>
    /// <param name="organizationUnitTypeRepository">The organization unit type repository.</param>
    public CreateOrganizationUnitTypeCommandHandler(IOrganizationUnitTypeRepository organizationUnitTypeRepository)
    {
        _organizationUnitTypeRepository = organizationUnitTypeRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<Guid>> Handle(CreateOrganizationUnitTypeCommand request, CancellationToken cancellationToken)
    {
        var existing = await _organizationUnitTypeRepository.ExistsByNameAsync(request.Name, cancellationToken);
        if (existing)
            return Result<Guid>.Failure(Error.Conflict("Organization unit type name already exists."));

        var organizationUnitType = OrganizationUnitType.Create(request.Name, request.Description);

        await _organizationUnitTypeRepository.AddAsync(organizationUnitType, cancellationToken);

        return Result<Guid>.Success(organizationUnitType.Id.Value);
    }
}
