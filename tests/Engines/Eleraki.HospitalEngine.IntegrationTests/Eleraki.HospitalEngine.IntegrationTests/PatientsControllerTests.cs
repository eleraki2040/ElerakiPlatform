using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using Eleraki.HospitalEngine.Application.DTOs;

namespace Eleraki.HospitalEngine.IntegrationTests;

public class PatientsControllerTests : IClassFixture<HospitalWebApplicationFactory>
{
    private readonly HospitalWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public PatientsControllerTests(HospitalWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreatePatient_Should_Return_Created_With_Id()
    {
        var request = new
        {
            Name = "John Doe",
            Email = "john@example.com",
            Phone = "+1234567890",
            DateOfBirth = "1990-01-01T00:00:00Z"
        };

        var response = await _client.PostAsJsonAsync("/api/Patients", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task CreatePatient_Should_Return_BadRequest_With_Invalid_Email()
    {
        var request = new
        {
            Name = "John Doe",
            Email = "invalid-email",
            Phone = "+1234567890",
            DateOfBirth = "1990-01-01T00:00:00Z"
        };

        var response = await _client.PostAsJsonAsync("/api/Patients", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreatePatient_Should_Return_BadRequest_With_Missing_Name()
    {
        var request = new
        {
            Name = "",
            Email = "john@example.com",
            Phone = "+1234567890",
            DateOfBirth = "1990-01-01T00:00:00Z"
        };

        var response = await _client.PostAsJsonAsync("/api/Patients", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetPatientById_Should_Return_NotFound_When_Patient_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Patients/{randomId}");

        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound, $"Expected 404 but got {(int)response.StatusCode}. Content: {content}");
    }

    [Fact]
    public async Task GetPatientById_Should_Return_Patient_When_Exists()
    {
        var createRequest = new
        {
            Name = "Jane Doe",
            Email = "jane@example.com",
            Phone = "+0987654321",
            DateOfBirth = "1985-06-15T00:00:00Z"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/Patients", createRequest);
        var createdId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var response = await _client.GetAsync($"/api/Patients/{createdId}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var patient = await response.Content.ReadFromJsonAsync<PatientDto>();
        patient.Should().NotBeNull();
        patient!.Id.Should().Be(createdId);
        patient.Name.Should().Be("Jane Doe");
        patient.Email.Should().Be("jane@example.com");
    }
}
