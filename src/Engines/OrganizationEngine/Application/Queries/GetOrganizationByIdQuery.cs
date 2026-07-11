using Eleraki.OrganizationEngine.Domain.Identity;
using Eleraki.OrganizationEngine.Domain.Repositories;
using Eleraki.OrganizationEngine.Application.DTOs;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.OrganizationEngine.Application.Queries;

/// <summary>
/// Query to get an organization by ID.
/// </summary>
/// <param name="Id">The organization identifier.</param>
public record GetOrganizationByIdQuery(OrganizationId Id) : IRequest<Result<OrganizationDto>>;

/// <summary>
/// Handles the GetOrganizationByIdQuery.
/// </summary>
public class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, Result<OrganizationDto>>
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
    public async Task<Result<OrganizationDto>> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(request.Id, cancellationToken);

        if (organization is null)
            return Result<OrganizationDto>.Failure(Error.NotFound("Organization not found."));

        var dto = new OrganizationDto
        {
            Id = organization.Id.Value,
            Name = organization.Name.Value,
            Code = organization.Code.Value,
            Description = organization.Description
        };

        return Result<OrganizationDto>.Success(dto);
    }
}