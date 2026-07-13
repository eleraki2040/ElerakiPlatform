namespace Eleraki.PurchasingEngine.Application.DTOs;

public class PurchaseOrderLineDto
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string Currency { get; set; } = "USD";
    public decimal LineTotal { get; set; }
}
