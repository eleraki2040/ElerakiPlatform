using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Eleraki.IdentityEngine.IntegrationTests;

public class UsersControllerTests : IClassFixture<IdentityWebApplicationFactory>
{
    private readonly IdentityWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public UsersControllerTests(IdentityWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateUser_Should_Return_Created_With_Id()
    {
        var request = new { Name = "Test User", Email = "test@example.com", Password = "Password123" };

        var response = await _client.PostAsJsonAsync("/api/Users", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, id);
    }

    [Fact]
    public async Task CreateUser_Should_Return_BadRequest_With_Invalid_Email()
    {
        var request = new { Name = "Test User", Email = "invalid-email", Password = "Password123" };

        var response = await _client.PostAsJsonAsync("/api/Users", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
