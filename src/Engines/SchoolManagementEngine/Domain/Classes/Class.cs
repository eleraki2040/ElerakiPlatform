using Eleraki.SchoolManagementEngine.Domain.Events;
using Eleraki.SchoolManagementEngine.Domain.Teachers;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.SchoolManagementEngine.Domain.Classes;

public sealed class Class : AggregateRoot<ClassId>
{
    public string Name { get; private set; } = string.Empty;
    public string Grade { get; private set; } = string.Empty;
    public TeacherId HomeroomTeacherId { get; private set; }
    public int MaxCapacity { get; private set; }
    public bool IsActive { get; private set; }

    private Class(ClassId id) : base(id)
    {
    }

    public static Class Create(string name, string grade, TeacherId homeroomTeacherId, int maxCapacity)
    {
        Guard.NotNullOrEmpty(name, nameof(name));
        Guard.NotNullOrEmpty(grade, nameof(grade));
        Guard.NotNull(homeroomTeacherId, nameof(homeroomTeacherId));

        var classEntity = new Class(ClassId.New())
        {
            Name = name,
            Grade = grade,
            HomeroomTeacherId = homeroomTeacherId,
            MaxCapacity = maxCapacity,
            IsActive = true
        };

        classEntity.RaiseDomainEvent(new ClassCreatedDomainEvent(classEntity.Id, Guid.NewGuid(), DateTime.UtcNow));

        return classEntity;
    }

    public void Update(string name, string grade, int maxCapacity)
    {
        Guard.NotNullOrEmpty(name, nameof(name));
        Guard.NotNullOrEmpty(grade, nameof(grade));

        Name = name;
        Grade = grade;
        MaxCapacity = maxCapacity;
    }

    public void AssignHomeroomTeacher(TeacherId teacherId)
    {
        Guard.NotNull(teacherId, nameof(teacherId));
        HomeroomTeacherId = teacherId;
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
