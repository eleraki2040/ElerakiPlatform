using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Eleraki.AuthorizationEngine.IntegrationTests;

/// <summary>
/// Integration tests for Authorization API endpoints.
/// </summary>
public class PermissionsControllerTests : IClassFixture<AuthorizationWebApplicationFactory>
{
    private readonly AuthorizationWebApplicationFactory _factory;
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="PermissionsControllerTests"/> class.
    /// </summary>
    /// <param name="factory">The web application factory.</param>
    public PermissionsControllerTests(AuthorizationWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Tests that creating a permission returns 201 Created with the permission ID.
    /// </summary>
    [Fact]
    public async Task CreatePermission_Should_Return_Created_With_Id()
    {
        var request = new { Name = "Users.Read", Code = "users:read", Description = "Read users", Resource = "users", Type = "read" };

        var response = await _client.PostAsJsonAsync("/api/Permissions", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
    }

    /// <summary>
    /// Tests that creating a permission with empty name returns 400 Bad Request.
    /// </summary>
    [Fact]
    public async Task CreatePermission_Should_Return_BadRequest_With_Empty_Name()
    {
        var request = new { Name = "", Code = "users:read", Description = "Read users" };

        var response = await _client.PostAsJsonAsync("/api/Permissions", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
