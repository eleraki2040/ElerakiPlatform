using Eleraki.Enterprise.Application.Commands;
using Eleraki.Enterprise.Application.Tests.Fakes;
using Eleraki.Enterprise.Domain;
using Eleraki.Enterprise.Domain.Repositories;
using Xunit;

namespace Eleraki.Enterprise.Application.Tests;

public class CreateEnterpriseCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Enterprise_And_Return_Id()
    {
        var repository = new FakeEnterpriseRepository();
        var handler = new CreateEnterpriseCommandHandler(repository);

        var command = new CreateEnterpriseCommand("ENT-APP-001", "Eleraki Application");

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value);
    }

    [Fact]
    public async Task Handle_Should_Fail_When_Code_Already_Exists()
    {
        var repository = new FakeEnterpriseRepository();
        var handler = new CreateEnterpriseCommandHandler(repository);

        await handler.Handle(new CreateEnterpriseCommand("ENT-APP-002", "First"), CancellationToken.None);
        var result = await handler.Handle(new CreateEnterpriseCommand("ENT-APP-002", "Duplicate"), CancellationToken.None);

        Assert.False(result.IsSuccess);
    }
}
