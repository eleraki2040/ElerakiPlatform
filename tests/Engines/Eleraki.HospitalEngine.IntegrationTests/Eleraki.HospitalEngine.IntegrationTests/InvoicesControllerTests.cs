using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace Eleraki.HospitalEngine.IntegrationTests;

public class InvoicesControllerTests : IClassFixture<HospitalWebApplicationFactory>
{
    private readonly HospitalWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public InvoicesControllerTests(HospitalWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GenerateInvoice_Should_Return_Created_With_Id()
    {
        var createRequest = new
        {
            Name = "Charlie Green",
            Email = "charlie@example.com",
            Phone = "+7771234567",
            DateOfBirth = "2000-07-04T00:00:00Z"
        };

        var createResponse = await _client.PostAsJsonAsync("/api/Patients", createRequest);
        var patientId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var invoiceRequest = new
        {
            PatientId = patientId,
            TotalAmount = 1500.00m,
            Currency = "USD",
            DueDate = DateTime.UtcNow.AddDays(30).ToString("O")
        };

        var response = await _client.PostAsJsonAsync("/api/Invoices", invoiceRequest);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var id = await response.Content.ReadFromJsonAsync<Guid>();
        id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task GetInvoiceById_Should_Return_NotFound_When_Invoice_Does_Not_Exist()
    {
        var randomId = Guid.NewGuid();
        var response = await _client.GetAsync($"/api/Invoices/{randomId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GenerateInvoice_Should_Return_BadRequest_With_Invalid_Data()
    {
        var request = new
        {
            PatientId = Guid.Empty,
            TotalAmount = -1m,
            Currency = "",
            DueDate = "2020-01-01T00:00:00Z"
        };

        var response = await _client.PostAsJsonAsync("/api/Invoices", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
