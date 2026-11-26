namespace BakeryManagement.Application.DTOs.OrderItems
{
    public class OrderItemSummaryDto
    {
        public Guid ProductId { get; set; }
        public string ProductNameAtOrder { get; set; } = string.Empty;
        public decimal UnitPriceAtOrder { get; set; }
        public int Quantity { get; set; }
    }
}
