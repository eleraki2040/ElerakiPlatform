using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace Eleraki.DeliveryEngine.IntegrationTests;

public class DeliveriesControllerTests : IClassFixture<DeliveryWebApplicationFactory>
{
    private readonly DeliveryWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public DeliveriesControllerTests(DeliveryWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateDelivery_Should_Return_Created_With_Id()
    {
        var request = new
        {
            RecipientName = "John Doe",
            DeliveryAddress = "123 Main St",
            ScheduledDate = DateTime.UtcNow.AddDays(1),
            Lines = new[]
            {
                new { ProductDescription = "Widget", Quantity = 10, UnitPrice = 100m, Currency = "USD" }
            }
        };

        var response = await _client.PostAsJsonAsync("/api/Deliveries", request);

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.Created, $"Expected 201 but got {(int)response.StatusCode}. Content: {content}");

        response.Headers.Location.Should().NotBeNull();

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateDelivery_Should_Return_BadRequest_With_Empty_RecipientName()
    {
        var request = new
        {
            RecipientName = "",
            DeliveryAddress = "123 Main St",
            ScheduledDate = DateTime.UtcNow.AddDays(1),
            Lines = new[]
            {
                new { ProductDescription = "Widget", Quantity = 10, UnitPrice = 100m, Currency = "USD" }
            }
        };

        var response = await _client.PostAsJsonAsync("/api/Deliveries", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetDeliveryById_Should_Return_NotFound_When_Delivery_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Deliveries/{randomId}");

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, $"Expected 404 but got {(int)response.StatusCode}. Content: {content}");
    }

    [Fact]
    public async Task GetDeliveryById_Should_Return_Delivery_When_Exists()
    {
        var createRequest = new
        {
            RecipientName = "Jane Doe",
            DeliveryAddress = "456 Oak Ave",
            ScheduledDate = DateTime.UtcNow.AddDays(2),
            Lines = new[]
            {
                new { ProductDescription = "Gadget", Quantity = 5, UnitPrice = 200m, Currency = "EUR" }
            }
        };

        var createResponse = await _client.PostAsJsonAsync("/api/Deliveries", createRequest);
        var createContent = await createResponse.Content.ReadAsStringAsync();
        createResponse.StatusCode.Should().Be(HttpStatusCode.Created, $"Expected 201 but got {(int)createResponse.StatusCode}. Content: {createContent}");

        var createdId = await createResponse.Content.ReadFromJsonAsync<Guid>();
        createdId.Should().NotBe(Guid.Empty);

        var response = await _client.GetAsync($"/api/Deliveries/{createdId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
