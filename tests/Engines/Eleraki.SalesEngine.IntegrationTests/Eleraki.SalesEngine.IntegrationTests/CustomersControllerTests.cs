using System.Net;
using FluentAssertions;
using Xunit;

namespace Eleraki.SalesEngine.IntegrationTests;

public class CustomersControllerTests : IClassFixture<SalesWebApplicationFactory>
{
    private readonly SalesWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public CustomersControllerTests(SalesWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetCustomerById_Should_Return_NotFound_When_Customer_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Customers/{randomId}");

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, $"Expected 404 but got {(int)response.StatusCode}. Content: {content}");
    }
}
