using Eleraki.HREngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.HREngine.Domain;

public sealed class Employee : AggregateRoot<EmployeeId>
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public string DepartmentId { get; private set; } = string.Empty;
    public string PositionId { get; private set; } = string.Empty;
    public string? SalaryId { get; private set; }
    public EmployeeStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Employee(EmployeeId id) : base(id)
    {
    }

    public static Employee Create(string firstName, string lastName, string email, string phone, DateTime dateOfBirth, string departmentId, string positionId, string? salaryId = null)
    {
        Guard.NotNullOrEmpty(firstName, nameof(firstName));
        Guard.NotNullOrEmpty(lastName, nameof(lastName));
        Guard.NotNullOrEmpty(email, nameof(email));
        Guard.NotNullOrEmpty(phone, nameof(phone));
        Guard.NotNullOrEmpty(departmentId, nameof(departmentId));
        Guard.NotNullOrEmpty(positionId, nameof(positionId));

        var employee = new Employee(EmployeeId.New())
        {
            FirstName = firstName.Trim(),
            LastName = lastName.Trim(),
            Email = email.Trim(),
            Phone = phone.Trim(),
            DateOfBirth = dateOfBirth,
            DepartmentId = departmentId.Trim(),
            PositionId = positionId.Trim(),
            SalaryId = salaryId,
            Status = EmployeeStatus.Active,
            CreatedAt = Clock.UtcNow,
            UpdatedAt = Clock.UtcNow
        };

        employee.RaiseDomainEvent(new EmployeeCreatedDomainEvent(employee.Id, Guid.NewGuid(), Clock.UtcNow));

        return employee;
    }

    public void Update(string firstName, string lastName, string email, string phone, string departmentId, string positionId, string? salaryId = null)
    {
        Guard.NotNullOrEmpty(firstName, nameof(firstName));
        Guard.NotNullOrEmpty(lastName, nameof(lastName));
        Guard.NotNullOrEmpty(email, nameof(email));
        Guard.NotNullOrEmpty(phone, nameof(phone));
        Guard.NotNullOrEmpty(departmentId, nameof(departmentId));
        Guard.NotNullOrEmpty(positionId, nameof(positionId));

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        Phone = phone.Trim();
        DepartmentId = departmentId.Trim();
        PositionId = positionId.Trim();
        SalaryId = salaryId;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new EmployeeUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Activate()
    {
        if (Status == EmployeeStatus.Active)
            return;

        Status = EmployeeStatus.Active;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new EmployeeActivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Deactivate()
    {
        if (Status == EmployeeStatus.Inactive)
            return;

        Status = EmployeeStatus.Inactive;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new EmployeeDeactivatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}

public enum EmployeeStatus
{
    Active = 1,
    Inactive = 2,
    Terminated = 3
}
