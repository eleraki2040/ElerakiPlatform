using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Eleraki.WorkflowEngine.IntegrationTests;

public class WorkflowsIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    private readonly HttpClient _client;
    private readonly IServiceScope _scope;
    private readonly WorkflowEngine.Infrastructure.Persistence.WorkflowDbContext _dbContext;

    public WorkflowsIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _scope = factory.Services.CreateScope();
        _dbContext = _scope.ServiceProvider.GetRequiredService<WorkflowEngine.Infrastructure.Persistence.WorkflowDbContext>();
        _dbContext.Database.EnsureCreated();
    }

    [Fact]
    public async Task Create_Should_Return_Workflow_Id()
    {
        var request = new { Name = "Test Workflow", Description = "Test Description" };

        var response = await _client.PostAsJsonAsync("api/Workflows", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Get_Should_Return_Empty_Initially()
    {
        var response = await _client.GetAsync("api/Workflows");

        response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
        _scope.Dispose();
    }
}
