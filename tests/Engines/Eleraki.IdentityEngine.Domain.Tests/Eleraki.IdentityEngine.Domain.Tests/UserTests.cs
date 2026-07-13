using Eleraki.IdentityEngine.Domain;
using Eleraki.SharedKernel.ValueObjects;
using Xunit;

namespace Eleraki.IdentityEngine.Domain.Tests;

public class UserTests
{
    [Fact]
    public void Create_Should_Return_User_With_Active_Status()
    {
        var name = UserName.Create("John Doe");
        var email = Email.Create("john@example.com");
        var password = UserPassword.Create("Password123!");

        var user = User.Create(name, email, password);

        Assert.NotNull(user);
        Assert.Equal(name.Value, user.Name.Value);
        Assert.Equal(email.Value, user.Email.Value);
        Assert.True(user.IsActive);
        Assert.Equal(UserRole.User, user.Role);
        Assert.NotEqual(default, user.Id);
    }

    [Fact]
    public void Create_Should_Raise_UserCreatedDomainEvent()
    {
        var name = UserName.Create("John Doe");
        var email = Email.Create("john@example.com");
        var password = UserPassword.Create("Password123!");

        var user = User.Create(name, email, password);

        Assert.Contains(user.DomainEvents, e => e.GetType().Name == "UserCreatedDomainEvent");
    }

    [Fact]
    public void Activate_Should_Set_IsActive_To_True()
    {
        var name = UserName.Create("John Doe");
        var email = Email.Create("john@example.com");
        var password = UserPassword.Create("Password123!");
        var user = User.Create(name, email, password);
        user.Deactivate();

        user.Activate();

        Assert.True(user.IsActive);
    }

    [Fact]
    public void Deactivate_Should_Set_IsActive_To_False()
    {
        var name = UserName.Create("John Doe");
        var email = Email.Create("john@example.com");
        var password = UserPassword.Create("Password123!");
        var user = User.Create(name, email, password);

        user.Deactivate();

        Assert.False(user.IsActive);
    }

    [Fact]
    public void ChangeRole_Should_Update_Role()
    {
        var name = UserName.Create("John Doe");
        var email = Email.Create("john@example.com");
        var password = UserPassword.Create("Password123!");
        var user = User.Create(name, email, password);

        user.ChangeRole(UserRole.Admin);

        Assert.Equal(UserRole.Admin, user.Role);
    }

    [Fact]
    public void ChangeRole_Should_Not_Update_When_Same_Role()
    {
        var name = UserName.Create("John Doe");
        var email = Email.Create("john@example.com");
        var password = UserPassword.Create("Password123!");
        var user = User.Create(name, email, password, UserRole.Manager);

        user.ChangeRole(UserRole.Manager);

        Assert.Equal(UserRole.Manager, user.Role);
    }
}
