using Eleraki.FinanceEngine.Domain;

namespace Eleraki.FinanceEngine.Domain;

public interface IJournalEntryRepository
{
    Task<JournalEntry?> GetByIdAsync(JournalEntryId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<JournalEntry>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(JournalEntry journalEntry, CancellationToken cancellationToken = default);
    Task UpdateAsync(JournalEntry journalEntry, CancellationToken cancellationToken = default);
    Task DeleteAsync(JournalEntry journalEntry, CancellationToken cancellationToken = default);
    Task AddLineAsync(JournalEntryLine line, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<JournalEntryLine>> GetLinesAsync(JournalEntryId journalEntryId, CancellationToken cancellationToken = default);
}
