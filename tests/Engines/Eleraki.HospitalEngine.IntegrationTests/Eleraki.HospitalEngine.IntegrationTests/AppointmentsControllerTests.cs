using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace Eleraki.HospitalEngine.IntegrationTests;

public class AppointmentsControllerTests : IClassFixture<HospitalWebApplicationFactory>
{
    private readonly HospitalWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public AppointmentsControllerTests(HospitalWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ScheduleAppointment_Should_Return_Created_With_Id()
    {
        var createRequest = new
        {
            Name = "Alice Smith",
            Email = "alice@example.com",
            Phone = "+1234567890",
            DateOfBirth = "1995-03-10T00:00:00Z"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/Patients", createRequest);
        var patientId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var appointmentRequest = new
        {
            PatientId = patientId,
            DoctorId = Guid.NewGuid(),
            ScheduledAt = DateTime.UtcNow.AddDays(30).ToString("O"),
            Notes = "Regular checkup"
        };

        var response = await _client.PostAsJsonAsync("/api/Appointments", appointmentRequest);

        var content = await response.Content.ReadAsStringAsync();
        if (!response.StatusCode.Equals(HttpStatusCode.Created))
        {
            Console.WriteLine($"Expected 201, got {(int)response.StatusCode}. Content: {content}");
        }

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task GetAppointmentById_Should_Return_NotFound_When_Appointment_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Appointments/{randomId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ScheduleAppointment_Should_Return_BadRequest_With_Invalid_Data()
    {
        var request = new
        {
            PatientId = Guid.Empty,
            DoctorId = Guid.Empty,
            ScheduledAt = "2020-01-01T00:00:00Z",
            Notes = (string?)null
        };

        var response = await _client.PostAsJsonAsync("/api/Appointments", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
