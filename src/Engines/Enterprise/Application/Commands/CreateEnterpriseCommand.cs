using Eleraki.Enterprise.Domain;
using Eleraki.Enterprise.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.Enterprise.Application.Commands;

/// <summary>
/// Command to create a new enterprise.
/// </summary>
/// <param name="Code">The enterprise code.</param>
/// <param name="Name">The enterprise name.</param>
/// <param name="LegalName">Optional legal name.</param>
public record CreateEnterpriseCommand(string Code, string Name, string? LegalName = null) : IRequest<Result<Guid>>;

/// <summary>
/// Handles the CreateEnterpriseCommand.
/// </summary>
public class CreateEnterpriseCommandHandler : IRequestHandler<CreateEnterpriseCommand, Result<Guid>>
{
    private readonly IEnterpriseRepository _enterpriseRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEnterpriseCommandHandler"/> class.
    /// </summary>
    /// <param name="enterpriseRepository">The enterprise repository.</param>
    public CreateEnterpriseCommandHandler(IEnterpriseRepository enterpriseRepository)
    {
        _enterpriseRepository = enterpriseRepository;
    }

    /// <inheritdoc/>
    public async Task<Result<Guid>> Handle(CreateEnterpriseCommand request, CancellationToken cancellationToken)
    {
        var existing = await _enterpriseRepository.ExistsByCodeAsync(request.Code, cancellationToken);
        if (existing)
            return Result<Guid>.Failure(EnterpriseErrors.CodeAlreadyExists);

        var code = Eleraki.Enterprise.Domain.EnterpriseCode.Create(request.Code);
        var name = Eleraki.Enterprise.Domain.EnterpriseName.Create(request.Name);

        var enterprise = Eleraki.Enterprise.Domain.Enterprise.Create(code, name, request.LegalName);

        await _enterpriseRepository.AddAsync(enterprise, cancellationToken);

        return Result<Guid>.Success(enterprise.Id.Value);
    }
}