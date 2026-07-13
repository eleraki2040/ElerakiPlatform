using Eleraki.FinanceEngine.Application.Commands;
using Eleraki.FinanceEngine.Domain;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.FinanceEngine.Application.Tests;

public class CreateAccountCommandHandlerTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly CreateAccountCommandHandler _handler;

    public CreateAccountCommandHandlerTests()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _handler = new CreateAccountCommandHandler(_accountRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Success_With_AccountId()
    {
        var result = await _handler.Handle(
            new CreateAccountCommand("Cash", "ACC-001", AccountType.Asset),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_Call_Repository_AddAsync()
    {
        var result = await _handler.Handle(
            new CreateAccountCommand("Cash", "ACC-001", AccountType.Asset),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _accountRepositoryMock.Verify(
            repo => repo.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Create_Account_With_ParentAccountId()
    {
        var parentId = AccountId.New();

        var result = await _handler.Handle(
            new CreateAccountCommand("Sub Account", "ACC-002", AccountType.Asset, parentId.Value.ToString()),
            CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _accountRepositoryMock.Verify(
            repo => repo.AddAsync(It.Is<Account>(a => a.ParentAccountId == parentId.Value.ToString()), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
