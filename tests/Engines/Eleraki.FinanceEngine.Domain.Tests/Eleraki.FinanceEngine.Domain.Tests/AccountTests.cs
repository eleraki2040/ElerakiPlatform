using Eleraki.FinanceEngine.Domain;
using FluentAssertions;

namespace Eleraki.FinanceEngine.Domain.Tests;

public class AccountTests
{
    [Fact]
    public void Create_Should_Return_Account_With_Correct_Properties()
    {
        var name = "Cash";
        var code = "ACC-001";
        var type = AccountType.Asset;

        var account = Account.Create(name, code, type);

        account.Should().NotBeNull();
        account.Name.Should().Be(name);
        account.Code.Should().Be(code.ToUpperInvariant());
        account.Type.Should().Be(type);
        account.ParentAccountId.Should().BeNull();
        account.IsActive.Should().BeTrue();
        account.Id.Should().NotBe(default);
    }

    [Fact]
    public void Create_Should_Trim_Code_Whitespace()
    {
        var account = Account.Create("Cash", "  ACC-001  ", AccountType.Asset);

        account.Code.Should().Be("ACC-001");
    }

    [Fact]
    public void Create_Should_Raise_AccountCreatedDomainEvent()
    {
        var account = Account.Create("Cash", "ACC-001", AccountType.Asset);

        account.DomainEvents.Should().Contain(e => e.GetType().Name == "AccountCreatedDomainEvent");
    }

    [Fact]
    public void Create_Should_Throw_When_Name_Is_Null()
    {
        var action = () => Account.Create(null!, "ACC-001", AccountType.Asset);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_Name_Is_Empty()
    {
        var action = () => Account.Create(string.Empty, "ACC-001", AccountType.Asset);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_Code_Is_Null()
    {
        var action = () => Account.Create("Cash", null!, AccountType.Asset);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Throw_When_Code_Is_Empty()
    {
        var action = () => Account.Create("Cash", string.Empty, AccountType.Asset);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_Should_Accept_ParentAccountId()
    {
        var parentId = AccountId.New();

        var account = Account.Create("Sub Account", "ACC-002", AccountType.Asset, parentId.Value.ToString());

        account.ParentAccountId.Should().Be(parentId.Value.ToString());
    }

    [Fact]
    public void Update_Should_Change_Name_And_ParentAccountId()
    {
        var account = Account.Create("Old Name", "ACC-001", AccountType.Asset);
        var newParentId = AccountId.New();

        account.Update("New Name", newParentId.Value.ToString());

        account.Name.Should().Be("New Name");
        account.ParentAccountId.Should().Be(newParentId.Value.ToString());
    }

    [Fact]
    public void Activate_Should_Set_IsActive_To_True()
    {
        var account = Account.Create("Cash", "ACC-001", AccountType.Asset);
        account.Deactivate();

        account.Activate();

        account.IsActive.Should().BeTrue();
    }

    [Fact]
    public void Deactivate_Should_Set_IsActive_To_False()
    {
        var account = Account.Create("Cash", "ACC-001", AccountType.Asset);

        account.Deactivate();

        account.IsActive.Should().BeFalse();
    }
}
