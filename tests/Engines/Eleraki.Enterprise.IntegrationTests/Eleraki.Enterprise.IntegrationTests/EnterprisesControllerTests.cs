using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Eleraki.Enterprise.IntegrationTests;

/// <summary>
/// Integration tests for Enterprise API endpoints.
/// </summary>
public class EnterprisesControllerTests : IClassFixture<EnterpriseWebApplicationFactory>
{
    private readonly EnterpriseWebApplicationFactory _factory;
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnterprisesControllerTests"/> class.
    /// </summary>
    /// <param name="factory">The web application factory.</param>
    public EnterprisesControllerTests(EnterpriseWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Tests that creating an enterprise returns 201 Created with the enterprise ID.
    /// </summary>
    [Fact]
    public async Task CreateEnterprise_Should_Return_Created_With_Id()
    {
        var request = new { Code = "ENT-INT-001", Name = "Integration Test Enterprise" };

        var response = await _client.PostAsJsonAsync("/api/Enterprises", request);

        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, id);
    }

    /// <summary>
    /// Tests that getting an enterprise by ID returns 200 OK with the enterprise data.
    /// </summary>
    [Fact]
    public async Task GetEnterpriseById_Should_Return_Ok_With_Enterprise()
    {
        var createRequest = new { Code = "ENT-INT-002", Name = "Get Test Enterprise" };
        var createResponse = await _client.PostAsJsonAsync("/api/Enterprises", createRequest);
        var id = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var getResponse = await _client.GetAsync($"/api/Enterprises/{id}");

        var content = await getResponse.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    /// <summary>
    /// Tests that getting a non-existent enterprise returns 404 Not Found.
    /// </summary>
    [Fact]
    public async Task GetEnterpriseById_Should_Return_NotFound_When_Enterprise_Does_Not_Exist()
    {
        var nonExistentId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Enterprises/{nonExistentId}");

        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
