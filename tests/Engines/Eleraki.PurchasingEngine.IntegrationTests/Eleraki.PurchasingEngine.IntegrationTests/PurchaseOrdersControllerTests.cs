using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace Eleraki.PurchasingEngine.IntegrationTests;

public class PurchaseOrdersControllerTests : IClassFixture<PurchasingWebApplicationFactory>
{
    private readonly PurchasingWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public PurchaseOrdersControllerTests(PurchasingWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreatePurchaseOrder_Should_Return_BadRequest_When_Vendor_Not_Found()
    {
        var request = new
        {
            VendorId = Guid.NewGuid()
        };

        var response = await _client.PostAsJsonAsync("/api/PurchaseOrders", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreatePurchaseOrder_Should_Return_BadRequest_With_Empty_VendorId()
    {
        var request = new
        {
            VendorId = Guid.Empty
        };

        var response = await _client.PostAsJsonAsync("/api/PurchaseOrders", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetPurchaseOrderById_Should_Return_NotFound_When_Order_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/PurchaseOrders/{randomId}");

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, $"Expected 404 but got {(int)response.StatusCode}. Content: {content}");
    }
}
