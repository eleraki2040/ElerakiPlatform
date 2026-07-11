using Eleraki.Enterprise.Domain;
using MediatR;

namespace Eleraki.Enterprise.Application.Parties.Commands;

/// <summary>
/// Command to create a new Party.
/// </summary>
/// <param name="Name">The party name.</param>
/// <param name="Type">The party type.</param>
public sealed record CreatePartyCommand(string Name, PartyType Type) : IRequest<Guid>;
