using Eleraki.FinanceEngine.Domain;
using Eleraki.SharedKernel.Primitives;
using MediatR;

namespace Eleraki.FinanceEngine.Application.Commands;

public record PostJournalEntryCommand(JournalEntryId JournalEntryId) : IRequest<Result<Guid>>;

public class PostJournalEntryCommandHandler : IRequestHandler<PostJournalEntryCommand, Result<Guid>>
{
    private readonly IJournalEntryRepository _repository;

    public PostJournalEntryCommandHandler(IJournalEntryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(PostJournalEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = await _repository.GetByIdAsync(request.JournalEntryId, cancellationToken);
        if (entry is null)
            return Result<Guid>.Failure(Error.NotFound("JournalEntry not found."));

        entry.Post();
        await _repository.UpdateAsync(entry, cancellationToken);

        return Result<Guid>.Success(entry.Id.Value);
    }
}
