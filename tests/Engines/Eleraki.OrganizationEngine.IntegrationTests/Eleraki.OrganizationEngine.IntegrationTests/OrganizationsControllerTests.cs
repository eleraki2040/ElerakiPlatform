using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using Eleraki.OrganizationEngine.Application.DTOs;

namespace Eleraki.OrganizationEngine.IntegrationTests;

public class OrganizationsControllerTests : IClassFixture<OrganizationWebApplicationFactory>
{
    private readonly OrganizationWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public OrganizationsControllerTests(OrganizationWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateOrganization_Should_Return_Created_With_Id()
    {
        var request = new
        {
            Name = "Acme Corp",
            Code = "ACME",
            Description = "A test organization"
        };

        var response = await _client.PostAsJsonAsync("/api/Organizations", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var location = response.Headers.Location.ToString();
        var idString = location.Split('/').Last();
        Guid.Parse(idString).Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateOrganization_Should_Return_BadRequest_With_Empty_Name()
    {
        var request = new
        {
            Name = "",
            Code = "ACME"
        };

        var response = await _client.PostAsJsonAsync("/api/Organizations", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateOrganization_Should_Return_BadRequest_With_Empty_Code()
    {
        var request = new
        {
            Name = "Acme Corp",
            Code = ""
        };

        var response = await _client.PostAsJsonAsync("/api/Organizations", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetOrganizationById_Should_Return_NotFound_When_Organization_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Organizations/{randomId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetOrganizationById_Should_Return_Organization_When_Exists()
    {
        var createRequest = new
        {
            Name = "Acme Corp",
            Code = "ACME2",
            Description = "A test organization"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/Organizations", createRequest);
        createResponse.EnsureSuccessStatusCode();
        var location = createResponse.Headers.Location!.ToString();
        var createdId = Guid.Parse(location.Split('/').Last());

        var response = await _client.GetAsync($"/api/Organizations/{createdId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var organization = await response.Content.ReadFromJsonAsync<OrganizationDto>();
        organization.Should().NotBeNull();
        organization!.Id.Should().Be(createdId);
        organization.Name.Should().Be("Acme Corp");
        organization.Code.Should().Be("ACME2");
        organization.Description.Should().Be("A test organization");
    }

    [Fact]
    public async Task CreateOrganization_With_Same_Code_Should_Return_BadRequest()
    {
        var request1 = new
        {
            Name = "Acme Corp",
            Code = "ACMESAME",
            Description = "First organization"
        };

        var request2 = new
        {
            Name = "Another Corp",
            Code = "ACMESAME",
            Description = "Second organization"
        };

        var response1 = await _client.PostAsJsonAsync("/api/Organizations", request1);
        response1.StatusCode.Should().Be(HttpStatusCode.Created);

        var response2 = await _client.PostAsJsonAsync("/api/Organizations", request2);
        response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
