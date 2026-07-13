using Eleraki.FinanceEngine.Domain;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.FinanceEngine.Application.Commands;

public record CreateJournalEntryCommand(string Description) : IRequest<Result<Guid>>;

public class CreateJournalEntryCommandHandler : IRequestHandler<CreateJournalEntryCommand, Result<Guid>>
{
    private readonly IJournalEntryRepository _repository;

    public CreateJournalEntryCommandHandler(IJournalEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateJournalEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = JournalEntry.Create(request.Description);

        await _repository.AddAsync(entry, cancellationToken);

        return Result<Guid>.Success(entry.Id.Value);
    }
}
