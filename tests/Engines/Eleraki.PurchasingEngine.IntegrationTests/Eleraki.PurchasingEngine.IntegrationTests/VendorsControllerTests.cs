using System.Net;
using FluentAssertions;
using Xunit;

namespace Eleraki.PurchasingEngine.IntegrationTests;

public class VendorsControllerTests : IClassFixture<PurchasingWebApplicationFactory>
{
    private readonly PurchasingWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public VendorsControllerTests(PurchasingWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetVendorById_Should_Return_NotFound_When_Vendor_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Vendors/{randomId}");

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, $"Expected 404 but got {(int)response.StatusCode}. Content: {content}");
    }
}
