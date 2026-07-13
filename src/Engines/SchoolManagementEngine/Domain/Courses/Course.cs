using Eleraki.SchoolManagementEngine.Domain.Events;
using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.SchoolManagementEngine.Domain.Courses;

public sealed class Course : AggregateRoot<CourseId>
{
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int Credits { get; private set; }
    public TeacherId TeacherId { get; private set; }
    public bool IsActive { get; private set; }

    private Course(CourseId id) : base(id)
    {
    }

    public static Course Create(string name, string code, string description, int credits, TeacherId teacherId)
    {
        Guard.NotNullOrEmpty(name, nameof(name));
        Guard.NotNullOrEmpty(code, nameof(code));
        Guard.NotNull(teacherId, nameof(teacherId));

        var course = new Course(CourseId.New())
        {
            Name = name,
            Code = code,
            Description = description,
            Credits = credits,
            TeacherId = teacherId,
            IsActive = true
        };

        course.RaiseDomainEvent(new CourseCreatedDomainEvent(course.Id, Guid.NewGuid(), DateTime.UtcNow));

        return course;
    }

    public void Update(string name, string description, int credits)
    {
        Guard.NotNullOrEmpty(name, nameof(name));

        Name = name;
        Description = description;
        Credits = credits;
    }

    public void AssignTeacher(TeacherId teacherId)
    {
        Guard.NotNull(teacherId, nameof(teacherId));
        TeacherId = teacherId;
    }

    public void Deactivate()
    {
        if (!IsActive) return;
        IsActive = false;
    }

    public void Activate()
    {
        if (IsActive) return;
        IsActive = true;
    }
}
