using Eleraki.FinanceEngine.Application.DTOs;
using Eleraki.FinanceEngine.Application.Queries;
using Eleraki.FinanceEngine.Domain;
using FluentAssertions;
using MediatR;
using Moq;

namespace Eleraki.FinanceEngine.Application.Tests;

public class GetAccountByIdQueryHandlerTests
{
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly GetAccountByIdQueryHandler _handler;

    public GetAccountByIdQueryHandlerTests()
    {
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _handler = new GetAccountByIdQueryHandler(_accountRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_AccountDto_When_Found()
    {
        var account = Account.Create("Cash", "ACC-001", AccountType.Asset);
        _accountRepositoryMock
            .Setup(repo => repo.GetByIdAsync(account.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(account);

        var result = await _handler.Handle(
            new GetAccountByIdQuery(account.Id.Value),
            CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(account.Id.Value);
        result.Name.Should().Be("Cash");
        result.Code.Should().Be("ACC-001");
        result.Type.Should().Be("Asset");
        result.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_Return_Null_When_NotFound()
    {
        var accountId = AccountId.New();
        _accountRepositoryMock
            .Setup(repo => repo.GetByIdAsync(accountId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Account?)null);

        var result = await _handler.Handle(
            new GetAccountByIdQuery(accountId.Value),
            CancellationToken.None);

        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_Should_Call_Repository_GetByIdAsync()
    {
        var accountId = AccountId.New();
        _accountRepositoryMock
            .Setup(repo => repo.GetByIdAsync(accountId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Account?)null);

        var result = await _handler.Handle(
            new GetAccountByIdQuery(accountId.Value),
            CancellationToken.None);

        _accountRepositoryMock.Verify(
            repo => repo.GetByIdAsync(accountId, It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
