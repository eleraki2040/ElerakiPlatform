using Eleraki.FinanceEngine.Domain;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;
using MediatR;

namespace Eleraki.FinanceEngine.Application.Commands;

public record CreateAccountCommand(string Name, string Code, AccountType Type, string? ParentAccountId = null) : IRequest<Result<Guid>>;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Result<Guid>>
{
    private readonly IAccountRepository _repository;

    public CreateAccountCommandHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = Account.Create(request.Name, request.Code, request.Type, request.ParentAccountId);

        await _repository.AddAsync(account, cancellationToken);

        return Result<Guid>.Success(account.Id.Value);
    }
}
