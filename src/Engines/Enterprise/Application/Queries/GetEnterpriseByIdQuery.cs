using Eleraki.Enterprise.Domain;
using Eleraki.Enterprise.Domain.Repositories;
using MediatR;

namespace Eleraki.Enterprise.Application.Queries;

/// <summary>
/// Query to get an enterprise by ID.
/// </summary>
/// <param name="Id">The enterprise ID.</param>
public sealed record GetEnterpriseByIdQuery(Guid Id) : IRequest<Eleraki.Enterprise.Domain.Enterprise?>;

/// <summary>
/// Handler for GetEnterpriseByIdQuery.
/// </summary>
public sealed class GetEnterpriseByIdQueryHandler : IRequestHandler<GetEnterpriseByIdQuery, Eleraki.Enterprise.Domain.Enterprise?>
{
    private readonly IEnterpriseRepository _enterpriseRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetEnterpriseByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="enterpriseRepository">The enterprise repository.</param>
    public GetEnterpriseByIdQueryHandler(IEnterpriseRepository enterpriseRepository)
    {
        _enterpriseRepository = enterpriseRepository;
    }

    /// <inheritdoc/>
    public async Task<Eleraki.Enterprise.Domain.Enterprise?> Handle(GetEnterpriseByIdQuery request, CancellationToken cancellationToken)
    {
        var enterpriseId = EnterpriseId.From(request.Id);
        return await _enterpriseRepository.GetByIdAsync(enterpriseId, cancellationToken);
    }
}
