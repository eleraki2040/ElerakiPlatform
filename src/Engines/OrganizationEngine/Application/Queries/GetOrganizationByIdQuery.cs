using Eleraki.OrganizationEngine.Application.DTOs;
using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.Organizations;
using Eleraki.OrganizationEngine.Domain.Repositories;
using MediatR;

namespace Eleraki.OrganizationEngine.Application.Queries;

/// <summary>
/// Query to get an organization by ID.
/// </summary>
/// <param name="Id">The organization ID.</param>
public record GetOrganizationByIdQuery(Guid Id) : IRequest<OrganizationDto?>;

/// <summary>
/// Handles the GetOrganizationByIdQuery.
/// </summary>
public class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, OrganizationDto?>
{
    private readonly IOrganizationRepository _organizationRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetOrganizationByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="organizationRepository">The organization repository.</param>
    public GetOrganizationByIdQueryHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    /// <inheritdoc/>
    public async Task<OrganizationDto?> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(
            OrganizationId.From(request.Id),
            cancellationToken);

        if (organization is null)
            return null;

        return new OrganizationDto
        {
            Id = organization.Id.Value,
            Name = organization.Name.Value,
            Code = organization.Code.Value,
            Description = organization.Description,
            Status = organization.Status.ToString()
        };
    }
}
