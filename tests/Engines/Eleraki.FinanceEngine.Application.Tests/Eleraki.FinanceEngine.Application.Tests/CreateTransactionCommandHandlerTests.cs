using Eleraki.FinanceEngine.Application.Commands;
using Eleraki.FinanceEngine.Domain;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.FinanceEngine.Application.Tests;

public class CreateTransactionCommandHandlerTests
{
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
    private readonly CreateTransactionCommandHandler _handler;

    public CreateTransactionCommandHandlerTests()
    {
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _handler = new CreateTransactionCommandHandler(_transactionRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_With_TransactionId()
    {
        var result = await _handler.Handle(
            new CreateTransactionCommand(AccountId.New(), TransactionType.Debit, 1000m, "USD"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_Call_Repository_AddAsync()
    {
        var result = await _handler.Handle(
            new CreateTransactionCommand(AccountId.New(), TransactionType.Debit, 1000m, "USD"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _transactionRepositoryMock.Verify(
            repo => repo.AddAsync(It.IsAny<Transaction>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Create_Transaction_With_Correct_Amount_And_Currency()
    {
        var accountId = AccountId.New();

        var result = await _handler.Handle(
            new CreateTransactionCommand(accountId, TransactionType.Debit, 2500m, "EUR", "Invoice payment"),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _transactionRepositoryMock.Verify(
            repo => repo.AddAsync(It.Is<Transaction>(t => t.Amount.Amount == 2500m && t.Amount.Currency == "EUR" && t.Description == "Invoice payment"), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
