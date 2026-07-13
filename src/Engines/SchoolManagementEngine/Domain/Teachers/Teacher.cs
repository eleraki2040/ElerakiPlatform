using Eleraki.SchoolManagementEngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.SchoolManagementEngine.Domain.Teachers;

public sealed class Teacher : AggregateRoot<TeacherId>
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Specialization { get; private set; } = string.Empty;
    public DateTime HireDate { get; private set; }
    public bool IsActive { get; private set; }

    private Teacher(TeacherId id) : base(id)
    {
    }

    public static Teacher Create(string firstName, string lastName, string email, string phoneNumber, string specialization)
    {
        Guard.NotNullOrEmpty(firstName, nameof(firstName));
        Guard.NotNullOrEmpty(lastName, nameof(lastName));
        Guard.NotNullOrEmpty(email, nameof(email));

        var teacher = new Teacher(TeacherId.New())
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Specialization = specialization,
            HireDate = DateTime.UtcNow,
            IsActive = true
        };

        teacher.RaiseDomainEvent(new TeacherHiredDomainEvent(teacher.Id, Guid.NewGuid(), DateTime.UtcNow));

        return teacher;
    }

    public void UpdateContactInfo(string email, string phoneNumber)
    {
        Guard.NotNullOrEmpty(email, nameof(email));

        Email = email;
        PhoneNumber = phoneNumber;
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
