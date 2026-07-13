using Eleraki.SchoolManagementEngine.Domain.Classes;
using Eleraki.SharedKernel.Events;

namespace Eleraki.SchoolManagementEngine.Domain.Events;

public sealed record ClassCreatedDomainEvent(ClassId ClassId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
