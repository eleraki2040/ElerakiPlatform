using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace Eleraki.HREngine.IntegrationTests;

public class DepartmentsControllerTests : IClassFixture<HRWebApplicationFactory>
{
    private readonly HRWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public DepartmentsControllerTests(HRWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateDepartment_Should_Return_Created_With_Id()
    {
        var request = new
        {
            Name = "Engineering"
        };

        var response = await _client.PostAsJsonAsync("/api/Departments", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateDepartment_Should_Return_BadRequest_With_Empty_Name()
    {
        var request = new
        {
            Name = ""
        };

        var response = await _client.PostAsJsonAsync("/api/Departments", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetDepartmentById_Should_Return_NotFound_When_Department_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Departments/{randomId}");

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, $"Expected 404 but got {(int)response.StatusCode}. Content: {content}");
    }

    [Fact]
    public async Task GetDepartmentById_Should_Return_Department_When_Exists()
    {
        var createRequest = new
        {
            Name = "Marketing"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/Departments", createRequest);
        var createdId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var response = await _client.GetAsync($"/api/Departments/{createdId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
