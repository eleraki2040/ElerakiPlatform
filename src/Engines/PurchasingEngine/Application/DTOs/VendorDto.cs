namespace Eleraki.PurchasingEngine.Application.DTOs;

public class VendorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? Address { get; set; }
    public string Status { get; set; } = string.Empty;
}
