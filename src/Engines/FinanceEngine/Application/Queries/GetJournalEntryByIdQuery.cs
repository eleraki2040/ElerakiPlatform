using Eleraki.FinanceEngine.Application.DTOs;
using Eleraki.FinanceEngine.Domain;
using MediatR;

namespace Eleraki.FinanceEngine.Application.Queries;

public record GetJournalEntryByIdQuery(Guid Id) : IRequest<JournalEntryDto?>;

public class GetJournalEntryByIdQueryHandler : IRequestHandler<GetJournalEntryByIdQuery, JournalEntryDto?>
{
    private readonly IJournalEntryRepository _repository;

    public GetJournalEntryByIdQueryHandler(IJournalEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<JournalEntryDto?> Handle(GetJournalEntryByIdQuery request, CancellationToken cancellationToken)
    {
        var entry = await _repository.GetByIdAsync(JournalEntryId.From(request.Id), cancellationToken);
        if (entry is null) return null;

        return new JournalEntryDto
        {
            Id = entry.Id.Value,
            ReferenceNumber = entry.ReferenceNumber,
            Description = entry.Description,
            Status = entry.Status.ToString(),
            EntryDate = entry.EntryDate,
            CreatedOn = entry.CreatedOn,
            ModifiedOn = entry.ModifiedOn
        };
    }
}
