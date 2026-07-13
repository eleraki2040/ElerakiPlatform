using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace Eleraki.FinanceEngine.IntegrationTests;

public class TransactionsControllerTests : IClassFixture<FinanceWebApplicationFactory>
{
    private readonly FinanceWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public TransactionsControllerTests(FinanceWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateTransaction_Should_Return_Created_With_Id()
    {
        var request = new
        {
            AccountId = new { Value = Guid.NewGuid() },
            Type = 1,
            Amount = 100m,
            Currency = "USD"
        };

        var response = await _client.PostAsJsonAsync("/api/Transactions", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateTransaction_Should_Return_BadRequest_With_Zero_Amount()
    {
        var request = new
        {
            AccountId = new { Value = Guid.NewGuid() },
            Type = 1,
            Amount = 0m,
            Currency = "USD"
        };

        var response = await _client.PostAsJsonAsync("/api/Transactions", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetTransactionById_Should_Return_NotFound_When_Transaction_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Transactions/{randomId}");

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, $"Expected 404 but got {(int)response.StatusCode}. Content: {content}");
    }

    [Fact]
    public async Task GetTransactionById_Should_Return_Transaction_When_Exists()
    {
        var createRequest = new
        {
            AccountId = new { Value = Guid.NewGuid() },
            Type = 2,
            Amount = 250m,
            Currency = "EUR"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/Transactions", createRequest);
        var createdId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var response = await _client.GetAsync($"/api/Transactions/{createdId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
