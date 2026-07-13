using Eleraki.HospitalEngine.Domain.Events;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.SharedKernel.Abstractions;
using Eleraki.SharedKernel.Entities;
using Eleraki.SharedKernel.Events;
using Eleraki.SharedKernel.Primitives;
using Eleraki.SharedKernel.ValueObjects;

namespace Eleraki.HospitalEngine.Domain.Billing;

public sealed class Invoice : AggregateRoot<InvoiceId>
{
    public PatientId PatientId { get; private set; }
    public InvoiceStatus Status { get; private set; }
    public Money TotalAmount { get; private set; } = null!;
    public Money PaidAmount { get; private set; } = null!;
    public DateTime DueDate { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime ModifiedOn { get; private set; }

    private Invoice(InvoiceId id) : base(id)
    {
        PatientId = default!;
        TotalAmount = default!;
        PaidAmount = default!;
    }

    private Invoice() : base(default!)
    {
    }

    public static Invoice Create(PatientId patientId, Money totalAmount, DateTime dueDate)
    {
        Guard.NotNull(patientId, nameof(patientId));
        Guard.NotNull(totalAmount, nameof(totalAmount));

        var invoice = new Invoice(InvoiceId.New())
        {
            PatientId = patientId,
            Status = InvoiceStatus.Pending,
            TotalAmount = totalAmount,
            PaidAmount = Money.Create(0, totalAmount.Currency),
            DueDate = dueDate,
            CreatedOn = Clock.UtcNow,
            ModifiedOn = Clock.UtcNow
        };

        invoice.RaiseDomainEvent(new InvoiceGeneratedDomainEvent(invoice.Id, Guid.NewGuid(), Clock.UtcNow));

        return invoice;
    }

    public void RecordPayment(Money amount)
    {
        Guard.NotNull(amount, nameof(amount));
        Guard.Ensure(amount.Currency == TotalAmount.Currency, "Currency mismatch.");

        var newPaidAmount = PaidAmount.Add(amount);
        PaidAmount = newPaidAmount;

        if (PaidAmount.Amount >= TotalAmount.Amount)
        {
            Status = InvoiceStatus.Paid;
        }
        else if (PaidAmount.Amount > 0)
        {
            Status = InvoiceStatus.PartiallyPaid;
        }

        ModifiedOn = Clock.UtcNow;

        RaiseDomainEvent(new PaymentReceivedDomainEvent(Id, Guid.NewGuid(), Clock.UtcNow));
    }

    public void Cancel()
    {
        if (Status == InvoiceStatus.Cancelled)
            return;

        Status = InvoiceStatus.Cancelled;
        ModifiedOn = Clock.UtcNow;
    }
}

public enum InvoiceStatus
{
    Pending = 1,
    PartiallyPaid = 2,
    Paid = 3,
    Cancelled = 4
}
