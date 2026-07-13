using Eleraki.FinanceEngine.Domain;
using Eleraki.SharedKernel.ValueObjects;
using FluentAssertions;

namespace Eleraki.FinanceEngine.Domain.Tests;

public class TransactionTests
{
    [Fact]
    public void Create_Should_Return_Transaction_With_Correct_Properties()
    {
        var accountId = AccountId.New();
        var money = Money.Create(1000m, "USD");
        var type = TransactionType.Debit;

        var transaction = Transaction.Create(accountId, type, money, "Test transaction");

        transaction.Should().NotBeNull();
        transaction.AccountId.Should().Be(accountId);
        transaction.Type.Should().Be(type);
        transaction.Amount.Should().Be(money);
        transaction.Description.Should().Be("Test transaction");
        transaction.Status.Should().Be(TransactionStatus.Draft);
        transaction.Id.Should().NotBe(default);
    }

    [Fact]
    public void Create_Should_Raise_TransactionCreatedDomainEvent()
    {
        var accountId = AccountId.New();
        var money = Money.Create(1000m, "USD");

        var transaction = Transaction.Create(accountId, TransactionType.Debit, money);

        transaction.DomainEvents.Should().Contain(e => e.GetType().Name == "TransactionCreatedDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_AccountId_Is_Null()
    {
        var money = Money.Create(1000m, "USD");

        var action = () => Transaction.Create(default!, TransactionType.Debit, money);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_Amount_Is_Null()
    {
        var accountId = AccountId.New();

        var action = () => Transaction.Create(accountId, TransactionType.Debit, null!);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Approve_Should_Change_Status_To_Approved()
    {
        var accountId = AccountId.New();
        var money = Money.Create(1000m, "USD");
        var transaction = Transaction.Create(accountId, TransactionType.Debit, money);

        transaction.Approve();

        transaction.Status.Should().Be(TransactionStatus.Approved);
    }

    [Fact]
    public void Approve_Should_Raise_TransactionApprovedDomainEvent()
    {
        var accountId = AccountId.New();
        var money = Money.Create(1000m, "USD");
        var transaction = Transaction.Create(accountId, TransactionType.Debit, money);

        transaction.Approve();

        transaction.DomainEvents.Should().Contain(e => e.GetType().Name == "TransactionApprovedDomainEvent");
    }

    [Fact]
    public void Approve_Should_Be_Idempotent()
    {
        var accountId = AccountId.New();
        var money = Money.Create(1000m, "USD");
        var transaction = Transaction.Create(accountId, TransactionType.Debit, money);
        transaction.Approve();

        transaction.Approve();

        transaction.Status.Should().Be(TransactionStatus.Approved);
    }

    [Fact]
    public void Post_Should_Change_Status_To_Posted()
    {
        var accountId = AccountId.New();
        var money = Money.Create(1000m, "USD");
        var transaction = Transaction.Create(accountId, TransactionType.Debit, money);

        transaction.Post();

        transaction.Status.Should().Be(TransactionStatus.Posted);
    }

    [Fact]
    public void Void_Should_Change_Status_To_Voided()
    {
        var accountId = AccountId.New();
        var money = Money.Create(1000m, "USD");
        var transaction = Transaction.Create(accountId, TransactionType.Debit, money);

        transaction.Void();

        transaction.Status.Should().Be(TransactionStatus.Voided);
    }
}
