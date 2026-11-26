using System.ComponentModel.DataAnnotations;

namespace BakeryManagement.Application.DTOs.OrderItems
{
    public class UpdateOrderItemDto
    {
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
