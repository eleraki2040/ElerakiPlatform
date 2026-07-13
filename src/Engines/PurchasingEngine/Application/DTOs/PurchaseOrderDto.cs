namespace Eleraki.PurchasingEngine.Application.DTOs;

public class PurchaseOrderDto
{
    public Guid Id { get; set; }
    public Guid VendorId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = "USD";
    public string? Notes { get; set; }
    public List<PurchaseOrderLineDto> Lines { get; set; } = new();
}
