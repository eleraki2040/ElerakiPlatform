using Eleraki.FinanceEngine.Domain;
using Eleraki.FinanceEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.FinanceEngine.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly FinanceDbContext _context;

    public TransactionRepository(FinanceDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetByIdAsync(TransactionId id, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions.FirstOrDefaultAsync(t => t.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<Transaction>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Transactions.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        await _context.Transactions.AddAsync(transaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
