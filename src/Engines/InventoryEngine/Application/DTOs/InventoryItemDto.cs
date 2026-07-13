namespace Eleraki.InventoryEngine.Application.DTOs;

public class InventoryItemDto
{
    public Guid Id { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public string? Location { get; set; }
    public Guid WarehouseId { get; set; }
    public string Status { get; set; } = string.Empty;
}
