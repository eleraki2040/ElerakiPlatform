using Eleraki.FinanceEngine.Domain;

namespace Eleraki.FinanceEngine.Domain;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(TransactionId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Transaction>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
    Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken = default);
    Task DeleteAsync(Transaction transaction, CancellationToken cancellationToken = default);
}
