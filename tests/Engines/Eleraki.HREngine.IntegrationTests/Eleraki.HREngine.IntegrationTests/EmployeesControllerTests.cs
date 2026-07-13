using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace Eleraki.HREngine.IntegrationTests;

public class EmployeesControllerTests : IClassFixture<HRWebApplicationFactory>
{
    private readonly HRWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public EmployeesControllerTests(HRWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateEmployee_Should_Return_Created_With_Id()
    {
        var request = new
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "+1234567890",
            DateOfBirth = "1990-01-01T00:00:00Z",
            DepartmentId = Guid.NewGuid().ToString(),
            PositionId = Guid.NewGuid().ToString()
        };

        var response = await _client.PostAsJsonAsync("/api/Employees", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreateEmployee_Should_Return_BadRequest_With_Empty_FirstName()
    {
        var request = new
        {
            FirstName = "",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Phone = "+1234567890",
            DateOfBirth = "1990-01-01T00:00:00Z",
            DepartmentId = Guid.NewGuid().ToString(),
            PositionId = Guid.NewGuid().ToString()
        };

        var response = await _client.PostAsJsonAsync("/api/Employees", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetEmployeeById_Should_Return_NotFound_When_Employee_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Employees/{randomId}");

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, $"Expected 404 but got {(int)response.StatusCode}. Content: {content}");
    }

    [Fact]
    public async Task GetEmployeeById_Should_Return_Employee_When_Exists()
    {
        var createRequest = new
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Phone = "+0987654321",
            DateOfBirth = "1985-06-15T00:00:00Z",
            DepartmentId = Guid.NewGuid().ToString(),
            PositionId = Guid.NewGuid().ToString()
        };

        var createResponse = await _client.PostAsJsonAsync("/api/Employees", createRequest);
        var createdId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var response = await _client.GetAsync($"/api/Employees/{createdId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
