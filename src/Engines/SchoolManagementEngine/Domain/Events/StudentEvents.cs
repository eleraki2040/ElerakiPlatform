using Eleraki.SchoolManagementEngine.Domain.Students;
using Eleraki.SharedKernel.Events;

namespace Eleraki.SchoolManagementEngine.Domain.Events;

public sealed record StudentEnrolledDomainEvent(StudentId StudentId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
