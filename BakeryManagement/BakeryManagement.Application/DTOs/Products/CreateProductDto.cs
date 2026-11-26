using System.ComponentModel.DataAnnotations;

namespace BakeryManagement.Application.DTOs.Products
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(200)]
        public required string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [StringLength(20)]
        public required string Category { get; set; }

        [Range(typeof(decimal), "0", "99999999.99")]
        public required decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;
    }
}
