using Eleraki.FinanceEngine.Domain;
using Eleraki.FinanceEngine.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Eleraki.FinanceEngine.Infrastructure.Repositories;

public class JournalEntryRepository : IJournalEntryRepository
{
    private readonly FinanceDbContext _context;

    public JournalEntryRepository(FinanceDbContext context)
    {
        _context = context;
    }

    public async Task<JournalEntry?> GetByIdAsync(JournalEntryId id, CancellationToken cancellationToken = default)
    {
        return await _context.JournalEntries.FirstOrDefaultAsync(j => j.Id.Value == id.Value, cancellationToken);
    }

    public async Task<IReadOnlyList<JournalEntry>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.JournalEntries.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(JournalEntry journalEntry, CancellationToken cancellationToken = default)
    {
        await _context.JournalEntries.AddAsync(journalEntry, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(JournalEntry journalEntry, CancellationToken cancellationToken = default)
    {
        _context.JournalEntries.Update(journalEntry);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(JournalEntry journalEntry, CancellationToken cancellationToken = default)
    {
        _context.JournalEntries.Remove(journalEntry);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddLineAsync(JournalEntryLine line, CancellationToken cancellationToken = default)
    {
        await _context.JournalEntryLines.AddAsync(line, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<JournalEntryLine>> GetLinesAsync(JournalEntryId journalEntryId, CancellationToken cancellationToken = default)
    {
        return await _context.JournalEntryLines
            .Where(l => l.JournalEntryId.Value == journalEntryId.Value)
            .ToListAsync(cancellationToken);
    }
}
