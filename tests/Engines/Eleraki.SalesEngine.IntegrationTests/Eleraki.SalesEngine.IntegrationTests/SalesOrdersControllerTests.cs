using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace Eleraki.SalesEngine.IntegrationTests;

public class SalesOrdersControllerTests : IClassFixture<SalesWebApplicationFactory>
{
    private readonly SalesWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public SalesOrdersControllerTests(SalesWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateSalesOrder_Should_Return_BadRequest_When_Customer_Not_Found()
    {
        var request = new
        {
            CustomerId = Guid.NewGuid(),
            CustomerName = "Test Customer",
            OrderNumber = "SO-001"
        };

        var response = await _client.PostAsJsonAsync("/api/SalesOrders", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateSalesOrder_Should_Return_BadRequest_With_Empty_CustomerId()
    {
        var request = new
        {
            CustomerId = Guid.Empty,
            CustomerName = "Test Customer",
            OrderNumber = "SO-002"
        };

        var response = await _client.PostAsJsonAsync("/api/SalesOrders", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetSalesOrderById_Should_Return_NotFound_When_Order_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/SalesOrders/{randomId}");

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, $"Expected 404 but got {(int)response.StatusCode}. Content: {content}");
    }
}
