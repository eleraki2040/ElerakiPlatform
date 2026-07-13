using Eleraki.FinanceEngine.Domain;

namespace Eleraki.FinanceEngine.Domain;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(AccountId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Account>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Account account, CancellationToken cancellationToken = default);
    Task UpdateAsync(Account account, CancellationToken cancellationToken = default);
    Task DeleteAsync(Account account, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
