using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Eleraki.InventoryEngine.IntegrationTests;

public class InventoryItemsControllerTests : IClassFixture<InventoryWebApplicationFactory>
{
    private readonly InventoryWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public InventoryItemsControllerTests(InventoryWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task CreateInventoryItem_Should_Return_Created_With_Id()
    {
        var request = new
        {
            Sku = "SKU-001",
            Name = "Test Item",
            Quantity = 10,
            WarehouseId = Guid.NewGuid()
        };

        var response = await _client.PostAsJsonAsync("/api/InventoryItems", request);

        var content = await response.Content.ReadAsStringAsync();
        _output.WriteLine($"Status: {(int)response.StatusCode}, Content: {content}");

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateInventoryItem_Should_Return_BadRequest_With_Empty_Sku()
    {
        var request = new
        {
            Sku = "",
            Name = "Test Item",
            Quantity = 10,
            WarehouseId = Guid.NewGuid()
        };

        var response = await _client.PostAsJsonAsync("/api/InventoryItems", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetInventoryItemById_Should_Return_NotFound_When_Item_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/InventoryItems/{randomId}");

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, $"Expected 404 but got {(int)response.StatusCode}. Content: {content}");
    }

    [Fact]
    public async Task GetInventoryItemById_Should_Return_Item_When_Exists()
    {
        var createRequest = new
        {
            Sku = "SKU-002",
            Name = "Test Item 2",
            Quantity = 5,
            WarehouseId = Guid.NewGuid()
        };

        var createResponse = await _client.PostAsJsonAsync("/api/InventoryItems", createRequest);
        var createdId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var response = await _client.GetAsync($"/api/InventoryItems/{createdId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
