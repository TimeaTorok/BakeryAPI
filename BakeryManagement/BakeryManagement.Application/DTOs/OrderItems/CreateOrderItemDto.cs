using System.ComponentModel.DataAnnotations;

namespace BakeryManagement.Application.DTOs.OrderItems
{
    public class CreateOrderItemDto
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
