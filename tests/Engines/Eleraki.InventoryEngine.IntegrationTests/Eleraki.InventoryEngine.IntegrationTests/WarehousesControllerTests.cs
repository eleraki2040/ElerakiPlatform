using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace Eleraki.InventoryEngine.IntegrationTests;

public class WarehousesControllerTests : IClassFixture<InventoryWebApplicationFactory>
{
    private readonly InventoryWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public WarehousesControllerTests(InventoryWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateWarehouse_Should_Return_Created_With_Id()
    {
        var request = new
        {
            Name = "Warehouse A",
            Code = "WH-A"
        };

        var response = await _client.PostAsJsonAsync("/api/Warehouses", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task GetWarehouseById_Should_Return_NotFound_When_Warehouse_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Warehouses/{randomId}");

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, $"Expected 404 but got {(int)response.StatusCode}. Content: {content}");
    }

    [Fact]
    public async Task GetWarehouseById_Should_Return_Warehouse_When_Exists()
    {
        var createRequest = new
        {
            Name = "Warehouse B",
            Code = "WH-B"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/Warehouses", createRequest);
        var createdId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var response = await _client.GetAsync($"/api/Warehouses/{createdId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
