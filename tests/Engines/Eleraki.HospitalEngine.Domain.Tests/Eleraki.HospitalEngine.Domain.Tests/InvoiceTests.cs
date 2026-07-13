using Eleraki.HospitalEngine.Domain.Billing;
using Eleraki.HospitalEngine.Domain.Patients;
using Eleraki.SharedKernel.ValueObjects;
using Xunit;

namespace Eleraki.HospitalEngine.Domain.Tests;

public class InvoiceTests
{
    [Fact]
    public void Create_Should_Return_Invoice_With_Pending_Status()
    {
        var patientId = PatientId.New();
        var totalAmount = Money.Create(1000, "EGP");
        var dueDate = DateTime.UtcNow.AddDays(30);

        var invoice = Invoice.Create(patientId, totalAmount, dueDate);

        Assert.NotNull(invoice);
        Assert.Equal(patientId, invoice.PatientId);
        Assert.Equal(1000, invoice.TotalAmount.Amount);
        Assert.Equal("EGP", invoice.TotalAmount.Currency);
        Assert.Equal(InvoiceStatus.Pending, invoice.Status);
        Assert.NotEqual(default, invoice.Id);
    }

    [Fact]
    public void RecordPayment_Should_Update_PaidAmount_And_Status()
    {
        var patientId = PatientId.New();
        var totalAmount = Money.Create(1000, "EGP");
        var dueDate = DateTime.UtcNow.AddDays(30);
        var invoice = Invoice.Create(patientId, totalAmount, dueDate);

        invoice.RecordPayment(Money.Create(600, "EGP"));

        Assert.Equal(600, invoice.PaidAmount.Amount);
        Assert.Equal(InvoiceStatus.PartiallyPaid, invoice.Status);
    }

    [Fact]
    public void RecordPayment_Should_Set_Status_To_Paid_When_Fully_Paid()
    {
        var patientId = PatientId.New();
        var totalAmount = Money.Create(1000, "EGP");
        var dueDate = DateTime.UtcNow.AddDays(30);
        var invoice = Invoice.Create(patientId, totalAmount, dueDate);

        invoice.RecordPayment(Money.Create(1000, "EGP"));

        Assert.Equal(1000, invoice.PaidAmount.Amount);
        Assert.Equal(InvoiceStatus.Paid, invoice.Status);
    }
}
