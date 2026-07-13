using Eleraki.FinanceEngine.Application.DTOs;
using Eleraki.FinanceEngine.Domain;
using MediatR;

namespace Eleraki.FinanceEngine.Application.Queries;

public record GetAccountByIdQuery(Guid Id) : IRequest<AccountDto?>;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, AccountDto?>
{
    private readonly IAccountRepository _repository;

    public GetAccountByIdQueryHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<AccountDto?> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetByIdAsync(AccountId.From(request.Id), cancellationToken);
        if (account is null) return null;

        return new AccountDto
        {
            Id = account.Id.Value,
            Name = account.Name,
            Code = account.Code,
            Type = account.Type.ToString(),
            ParentAccountId = account.ParentAccountId,
            IsActive = account.IsActive,
            CreatedOn = account.CreatedOn,
            ModifiedOn = account.ModifiedOn
        };
    }
}
