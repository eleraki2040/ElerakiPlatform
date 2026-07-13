using Eleraki.SchoolManagementEngine.Domain.Courses;
using Eleraki.SharedKernel.Events;

namespace Eleraki.SchoolManagementEngine.Domain.Events;

public sealed record CourseCreatedDomainEvent(CourseId CourseId, Guid Id, DateTime OccurredOn) : DomainEvent(Id, OccurredOn);
