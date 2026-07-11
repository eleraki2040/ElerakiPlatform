using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Eleraki.Enterprise.Infrastructure.Persistence;
using Eleraki.Enterprise.Infrastructure.Repositories;
using Eleraki.Enterprise.Domain;
using Eleraki.Enterprise.Domain.Repositories;

namespace Eleraki.Enterprise.Infrastructure.Tests;

public class EnterpriseRepositoryTests : IAsyncLifetime
{
    private readonly SqliteConnection _connection;
    private EnterpriseDbContext _context = null!;
    private EnterpriseRepository _repository = null!;

    public EnterpriseRepositoryTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
    }

    public async Task InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<EnterpriseDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new EnterpriseDbContext(options);
        await _context.Database.EnsureCreatedAsync();

        _repository = new EnterpriseRepository(_context);
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _connection.DisposeAsync();
    }

    [Fact]
    public async Task AddAsync_Should_Persist_Enterprise()
    {
        var code = global::Eleraki.Enterprise.Domain.EnterpriseCode.Create("ENT-INF-001");
        var name = global::Eleraki.Enterprise.Domain.EnterpriseName.Create("Infrastructure Test");
        var enterprise = global::Eleraki.Enterprise.Domain.Enterprise.Create(code, name);

        await _repository.AddAsync(enterprise);

        var saved = await _repository.GetByIdAsync(enterprise.Id);
        Assert.NotNull(saved);
        Assert.Equal("ENT-INF-001", saved.Code.Value);
    }

    [Fact]
    public async Task ExistsByCodeAsync_Should_Return_True_When_Code_Exists()
    {
        var code = global::Eleraki.Enterprise.Domain.EnterpriseCode.Create("ENT-INF-002");
        var name = global::Eleraki.Enterprise.Domain.EnterpriseName.Create("Infrastructure Test 2");
        var enterprise = global::Eleraki.Enterprise.Domain.Enterprise.Create(code, name);

        await _repository.AddAsync(enterprise);

        var exists = await _repository.ExistsByCodeAsync("ENT-INF-002");
        Assert.True(exists);
    }

    [Fact]
    public async Task GetByCodeAsync_Should_Return_Enterprise_When_Code_Exists()
    {
        var code = global::Eleraki.Enterprise.Domain.EnterpriseCode.Create("ENT-INF-003");
        var name = global::Eleraki.Enterprise.Domain.EnterpriseName.Create("Infrastructure Test 3");
        var enterprise = global::Eleraki.Enterprise.Domain.Enterprise.Create(code, name);

        await _repository.AddAsync(enterprise);

        var found = await _repository.GetByCodeAsync("ENT-INF-003");
        Assert.NotNull(found);
        Assert.Equal("Infrastructure Test 3", found.Name.Value);
    }
}
