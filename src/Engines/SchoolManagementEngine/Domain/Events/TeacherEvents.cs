using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Eleraki.SharedKernel.Events;

namespace Eleraki.SchoolManagementEngine.Domain.Events;

public sealed record TeacherHiredDomainEvent(TeacherId TeacherId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
