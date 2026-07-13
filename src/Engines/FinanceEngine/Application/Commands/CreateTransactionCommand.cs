using Eleraki.FinanceEngine.Domain;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;
using MediatR;

namespace Eleraki.FinanceEngine.Application.Commands;

public record CreateTransactionCommand(AccountId AccountId, TransactionType Type, decimal Amount, string Currency, string? Description = null) : IRequest<Result<Guid>>;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Result<Guid>>
{
    private readonly ITransactionRepository _repository;

    public CreateTransactionCommandHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var money = Money.Create(request.Amount, request.Currency);
        var transaction = Transaction.Create(request.AccountId, request.Type, money, request.Description);

        await _repository.AddAsync(transaction, cancellationToken);

        return Result<Guid>.Success(transaction.Id.Value);
    }
}
