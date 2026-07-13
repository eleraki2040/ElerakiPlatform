using Eleraki.FinanceEngine.Domain;
using Eleraki.FinanceEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.FinanceEngine.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly FinanceDbContext _context;

    public AccountRepository(FinanceDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByIdAsync(AccountId id, CancellationToken cancellationToken = default)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Account>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Accounts.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Account account, CancellationToken cancellationToken = default)
    {
        await _context.Accounts.AddAsync(account, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Account account, CancellationToken cancellationToken = default)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Account account, CancellationToken cancellationToken = default)
    {
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _context.Accounts.AnyAsync(a => a.Code == code, cancellationToken);
    }
}
