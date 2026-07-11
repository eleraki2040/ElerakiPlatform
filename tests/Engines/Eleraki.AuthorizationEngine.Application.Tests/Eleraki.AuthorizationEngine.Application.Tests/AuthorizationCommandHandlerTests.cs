using Eleraki.AuthorizationEngine.Application.Permissions.Commands;
using Eleraki.AuthorizationEngine.Application.Roles.Commands;
using Eleraki.AuthorizationEngine.Application.Tests.Fakes;
using Eleraki.AuthorizationEngine.Domain;
using Xunit;

namespace Eleraki.AuthorizationEngine.Application.Tests;

public class CreatePermissionCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Permission_And_Return_Id()
    {
        var repository = new FakePermissionRepository();
        var handler = new CreatePermissionCommandHandler(repository);

        var command = new CreatePermissionCommand("Users.Read", "users:read", "Read users", "users", "read");

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, result);
    }
}

public class CreateRoleCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Role_And_Return_Id()
    {
        var repository = new FakeRoleRepository();
        var handler = new CreateRoleCommandHandler(repository);

        var command = new CreateRoleCommand("Admin", "Administrator role");

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, result);
    }
}
