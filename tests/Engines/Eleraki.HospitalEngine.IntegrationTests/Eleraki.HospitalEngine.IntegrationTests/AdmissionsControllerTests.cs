using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace Eleraki.HospitalEngine.IntegrationTests;

public class AdmissionsControllerTests : IClassFixture<HospitalWebApplicationFactory>
{
    private readonly HospitalWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public AdmissionsControllerTests(HospitalWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateAdmission_Should_Return_Created_With_Id()
    {
        var createRequest = new
        {
            Name = "Bob Brown",
            Email = "bob@example.com",
            Phone = "+5551234567",
            DateOfBirth = "1978-11-22T00:00:00Z"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/Patients", createRequest);
        var patientId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var admissionRequest = new
        {
            PatientId = patientId,
            BedId = (Guid?)Guid.NewGuid(),
            Notes = "Scheduled surgery"
        };

        var response = await _client.PostAsJsonAsync("/api/Admissions", admissionRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task GetAdmissionById_Should_Return_NotFound_When_Admission_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Admissions/{randomId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateAdmission_Should_Return_BadRequest_With_Invalid_PatientId()
    {
        var request = new
        {
            PatientId = Guid.Empty,
            BedId = (Guid?)null,
            Notes = (string?)null
        };

        var response = await _client.PostAsJsonAsync("/api/Admissions", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
