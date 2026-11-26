using System.ComponentModel.DataAnnotations;

namespace BakeryManagement.Application.DTOs.Orders
{
    public class UpdateOrderDto
    {
        [Required]
        [StringLength(20)]
        public required string Status { get; set; }

        [StringLength(2000)]
        public string? Notes { get; set; }
    }
}
