using Eleraki.FinanceEngine.Domain;
using Eleraki.SharedKernel.ValueObjects;
using FluentAssertions;

namespace Eleraki.FinanceEngine.Domain.Tests;

public class JournalEntryTests
{
    [Fact]
    public void Create_Should_Return_JournalEntry_With_Correct_Properties()
    {
        var description = "Monthly payroll";

        var entry = JournalEntry.Create(description);

        entry.Should().NotBeNull();
        entry.Description.Should().Be(description);
        entry.Status.Should().Be(JournalEntryStatus.Draft);
        entry.Id.Should().NotBe(default);
        entry.ReferenceNumber.Should().StartWith("JE-");
    }

    [Fact]
    public void Create_Should_Raise_JournalEntryCreatedDomainEvent()
    {
        var entry = JournalEntry.Create("Monthly payroll");

        entry.DomainEvents.Should().Contain(e => e.GetType().Name == "JournalEntryCreatedDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_Description_Is_Null()
    {
        var action = () => JournalEntry.Create(null!);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_Description_Is_Empty()
    {
        var action = () => JournalEntry.Create(string.Empty);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Post_Should_Change_Status_To_Posted()
    {
        var entry = JournalEntry.Create("Monthly payroll");

        entry.Post();

        entry.Status.Should().Be(JournalEntryStatus.Posted);
    }

    [Fact]
    public void Post_Should_Raise_JournalEntryPostedDomainEvent()
    {
        var entry = JournalEntry.Create("Monthly payroll");

        entry.Post();

        entry.DomainEvents.Should().Contain(e => e.GetType().Name == "JournalEntryPostedDomainEvent");
    }

    [Fact]
    public void AddLine_Should_Add_Line_With_Correct_Properties()
    {
        var entry = JournalEntry.Create("Monthly payroll");
        var accountId = AccountId.New();
        var amount = Money.Create(5000m, "USD");

        var line = JournalEntryLine.Create(1, accountId, amount, "Salary", JournalEntryLineType.Debit);

        line.Should().NotBeNull();
        line.LineNumber.Should().Be(1);
        line.AccountId.Should().Be(accountId);
        line.Amount.Should().Be(amount);
        line.Description.Should().Be("Salary");
        line.Type.Should().Be(JournalEntryLineType.Debit);
    }
}
