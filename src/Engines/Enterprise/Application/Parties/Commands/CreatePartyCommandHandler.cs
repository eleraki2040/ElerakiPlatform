using Eleraki.Enterprise.Domain;
using Eleraki.Enterprise.Domain.Repositories;
using MediatR;

namespace Eleraki.Enterprise.Application.Parties.Commands;

/// <summary>
/// Handler for CreatePartyCommand.
/// </summary>
public sealed class CreatePartyCommandHandler : IRequestHandler<CreatePartyCommand, Guid>
{
    private readonly IPartyRepository _partyRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePartyCommandHandler"/> class.
    /// </summary>
    /// <param name="partyRepository">The party repository.</param>
    public CreatePartyCommandHandler(IPartyRepository partyRepository)
    {
        _partyRepository = partyRepository;
    }

    /// <inheritdoc/>
    public async Task<Guid> Handle(CreatePartyCommand request, CancellationToken cancellationToken)
    {
        var partyName = PartyName.Create(request.Name);

        var party = Party.Create(partyName, request.Type);

        await _partyRepository.AddAsync(party, cancellationToken);

        return party.Id.Value;
    }
}
