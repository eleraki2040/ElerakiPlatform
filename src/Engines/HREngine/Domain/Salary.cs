using Eleraki.HREngine.Domain.Events;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;

namespace Eleraki.HREngine.Domain;

public sealed class Salary : AggregateRoot<SalaryId>
{
    public string EmployeeId { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = string.Empty;
    public string? PayGrade { get; private set; }
    public DateTime EffectiveFrom { get; private set; }
    public DateTime? EffectiveTo { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Salary(SalaryId id) : base(id)
    {
    }

    public static Salary Create(string employeeId, decimal amount, string currency, string? payGrade = null, DateTime? effectiveTo = null)
    {
        Guard.NotNullOrEmpty(employeeId, nameof(employeeId));
        Guard.NotNullOrEmpty(currency, nameof(currency));
        Guard.Ensure(amount > 0, $"{nameof(amount)} must be greater than zero.");

        var salary = new Salary(SalaryId.New())
        {
            EmployeeId = employeeId,
            Amount = amount,
            Currency = currency,
            PayGrade = payGrade,
            EffectiveFrom = Clock.UtcNow,
            EffectiveTo = effectiveTo,
            CreatedAt = Clock.UtcNow,
            UpdatedAt = Clock.UtcNow
        };

        salary.RaiseDomainEvent(new SalaryCreatedDomainEvent(salary.Id, Guid.NewGuid(), Clock.UtcNow));

        return salary;
    }

    public void Update(decimal amount, string? payGrade = null, DateTime? effectiveTo = null)
    {
        Guard.Ensure(amount > 0, $"{nameof(amount)} must be greater than zero.");

        Amount = amount;
        PayGrade = payGrade;
        EffectiveTo = effectiveTo;
        UpdatedAt = Clock.UtcNow;

        RaiseDomainEvent(new SalaryUpdatedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }
}
