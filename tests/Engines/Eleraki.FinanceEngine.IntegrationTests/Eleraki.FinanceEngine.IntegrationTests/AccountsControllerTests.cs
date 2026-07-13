using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace Eleraki.FinanceEngine.IntegrationTests;

public class AccountsControllerTests : IClassFixture<FinanceWebApplicationFactory>
{
    private readonly FinanceWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public AccountsControllerTests(FinanceWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateAccount_Should_Return_Created_With_Id()
    {
        var request = new
        {
            Name = "Cash Account",
            Code = "1000",
            Type = 1
        };

        var response = await _client.PostAsJsonAsync("/api/Accounts", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateAccount_Should_Return_BadRequest_With_Empty_Name()
    {
        var request = new
        {
            Name = "",
            Code = "1001",
            Type = 1
        };

        var response = await _client.PostAsJsonAsync("/api/Accounts", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetAccountById_Should_Return_NotFound_When_Account_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Accounts/{randomId}");

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, $"Expected 404 but got {(int)response.StatusCode}. Content: {content}");
    }

    [Fact]
    public async Task GetAccountById_Should_Return_Account_When_Exists()
    {
        var createRequest = new
        {
            Name = "Bank Account",
            Code = "2000",
            Type = 1
        };

        var createResponse = await _client.PostAsJsonAsync("/api/Accounts", createRequest);
        var createdId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var response = await _client.GetAsync($"/api/Accounts/{createdId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
