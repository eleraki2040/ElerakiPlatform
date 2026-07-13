using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace Eleraki.DeliveryEngine.IntegrationTests;

public class DriversControllerTests : IClassFixture<DeliveryWebApplicationFactory>
{
    private readonly DeliveryWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public DriversControllerTests(DeliveryWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Create_Should_Return_Ok_With_Valid_Data()
    {
        var request = new
        {
            FullName = "John Doe",
            LicenseNumber = "DL-12345",
            Phone = "+1234567890",
            Email = "john@example.com"
        };

        var response = await _client.PostAsJsonAsync("/api/Drivers", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Create_Should_Return_BadRequest_With_Empty_FullName()
    {
        var request = new
        {
            FullName = "",
            LicenseNumber = "DL-12345",
            Phone = "+1234567890",
            Email = "john@example.com"
        };

        var response = await _client.PostAsJsonAsync("/api/Drivers", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetById_Should_Return_NotFound_When_Driver_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Drivers/{randomId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetById_Should_Return_Ok_After_Creation()
    {
        var createRequest = new
        {
            FullName = "Jane Doe",
            LicenseNumber = "DL-67890",
            Phone = "+0987654321",
            Email = "jane@example.com"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/Drivers", createRequest);
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var createdId = await createResponse.Content.ReadFromJsonAsync<Guid>();
        var response = await _client.GetAsync($"/api/Drivers/{createdId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
