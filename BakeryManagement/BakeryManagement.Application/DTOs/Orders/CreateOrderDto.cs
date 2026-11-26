using System.ComponentModel.DataAnnotations;

namespace BakeryManagement.Application.DTOs.Orders
{
    public class CreateOrderDto
    {
        [Required]
        public Guid CustomerId { get; set; }

        [StringLength(2000)]
        public string? Notes { get; set; }
    }
}
