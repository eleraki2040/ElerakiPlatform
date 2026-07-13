using Eleraki.SchoolManagementEngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.SchoolManagementEngine.Domain.Students;

public sealed class Student : AggregateRoot<StudentId>
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public string Address { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public DateTime EnrollmentDate { get; private set; }
    public bool IsActive { get; private set; }

    private Student(StudentId id) : base(id)
    {
    }

    public static Student Create(string firstName, string lastName, string email, DateTime dateOfBirth, string address, string phoneNumber)
    {
        Guard.NotNullOrEmpty(firstName, nameof(firstName));
        Guard.NotNullOrEmpty(lastName, nameof(lastName));
        Guard.NotNullOrEmpty(email, nameof(email));

        var student = new Student(StudentId.New())
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            DateOfBirth = dateOfBirth,
            Address = address,
            PhoneNumber = phoneNumber,
            EnrollmentDate = DateTime.UtcNow,
            IsActive = true
        };

        student.RaiseDomainEvent(new StudentEnrolledDomainEvent(student.Id, Guid.NewGuid(), DateTime.UtcNow));

        return student;
    }

    public void UpdateContactInfo(string email, string phoneNumber, string address)
    {
        Guard.NotNullOrEmpty(email, nameof(email));

        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
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
