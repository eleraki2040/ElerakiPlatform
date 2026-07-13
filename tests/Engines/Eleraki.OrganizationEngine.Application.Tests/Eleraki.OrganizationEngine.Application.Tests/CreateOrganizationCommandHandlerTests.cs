using Eleraki.OrganizationEngine.Application.Commands;
using Eleraki.OrganizationEngine.Domain.Organizations;
using Eleraki.OrganizationEngine.Domain.Repositories;
using Eleraki.SharedKernel.Primitives;
using MediatR;
using Moq;

namespace Eleraki.OrganizationEngine.Application.Tests;

public class CreateOrganizationCommandHandlerTests
{
    private readonly Mock<IOrganizationRepository> _organizationRepositoryMock;
    private readonly CreateOrganizationCommandHandler _handler;

    public CreateOrganizationCommandHandlerTests()
    {
        _organizationRepositoryMock = new Mock<IOrganizationRepository>();
        _handler = new CreateOrganizationCommandHandler(_organizationRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Create_Organization_When_Code_Does_Not_Exist()
    {
        _organizationRepositoryMock
            .Setup(repo => repo.ExistsByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _handler.Handle(
            new CreateOrganizationCommand("Eleraki", "ORG-001", "Description"),
            CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Code_Already_Exists()
    {
        _organizationRepositoryMock
            .Setup(repo => repo.ExistsByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _handler.Handle(
            new CreateOrganizationCommand("Eleraki", "ORG-001"),
            CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("Conflict", result.Error.Code);
    }

    [Fact]
    public async Task Handle_Should_Call_Repository_AddAsync()
    {
        _organizationRepositoryMock
            .Setup(repo => repo.ExistsByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _handler.Handle(
            new CreateOrganizationCommand("Eleraki", "ORG-001"),
            CancellationToken.None);

        Assert.True(result.IsSuccess);
        _organizationRepositoryMock.Verify(
            repo => repo.AddAsync(It.IsAny<Organization>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
