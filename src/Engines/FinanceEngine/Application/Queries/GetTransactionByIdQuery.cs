using Eleraki.FinanceEngine.Application.DTOs;
using Eleraki.FinanceEngine.Domain;
using MediatR;

namespace Eleraki.FinanceEngine.Application.Queries;

public record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionDto?>;

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto?>
{
    private readonly ITransactionRepository _repository;

    public GetTransactionByIdQueryHandler(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public async Task<TransactionDto?> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _repository.GetByIdAsync(TransactionId.From(request.Id), cancellationToken);
        if (transaction is null) return null;

        return new TransactionDto
        {
            Id = transaction.Id.Value,
            AccountId = transaction.AccountId.Value,
            Type = transaction.Type.ToString(),
            Amount = transaction.Amount.Amount,
            Currency = transaction.Amount.Currency,
            Description = transaction.Description,
            Status = transaction.Status.ToString(),
            TransactionDate = transaction.TransactionDate,
            CreatedOn = transaction.CreatedOn,
            ModifiedOn = transaction.ModifiedOn
        };
    }
}
