using Xunit;
using Eleraki.IdentityEngine.Application.Users.Commands;
using Eleraki.IdentityEngine.Domain;
using Eleraki.IdentityEngine.Domain.Repositories;
using Eleraki.IdentityEngine.Application.Tests.Fakes;

namespace Eleraki.IdentityEngine.Application.Tests;

public class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_User_And_Return_Id()
    {
        var repository = new FakeUserRepository();
        var handler = new CreateUserCommandHandler(repository);

        var command = new CreateUserCommand("Test User", "test@example.com", "Password123");

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, result);
    }
}
