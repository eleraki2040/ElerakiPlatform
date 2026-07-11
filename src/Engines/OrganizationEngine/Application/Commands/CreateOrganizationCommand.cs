using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.Repositories;
using Eleraki.OrganizationEngine.Domain.Organizations;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.OrganizationEngine.Application.Commands;

/// <summary>
/// Command to create a new organization.
/// </summary>
/// <param name="Name">The organization name.</param>
/// <param name="Code">The organization code.</param>
/// <param name="Description">Optional description.</param>
public record CreateOrganizationCommand(string Name, string Code, string? Description = null) : IRequest<Result<Guid>>;

/// <summary>
/// Handles the CreateOrganizationCommand.
/// </summary>
public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Result<Guid>>
{
    private readonly IOrganizationRepository _organizationRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateOrganizationCommandHandler"/> class.
    /// </summary>
    /// <param name="organizationRepository">The organization repository.</param>
    public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<Guid>> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var existing = await _organizationRepository.ExistsByCodeAsync(request.Code, cancellationToken);
        if (existing)
            return Result<Guid>.Failure(Error.Conflict("Organization code already exists."));

        var organization = Organization.Create(request.Name, request.Code, request.Description);

        await _organizationRepository.AddAsync(organization, cancellationToken);

        return Result<Guid>.Success(organization.Id.Value);
    }
}