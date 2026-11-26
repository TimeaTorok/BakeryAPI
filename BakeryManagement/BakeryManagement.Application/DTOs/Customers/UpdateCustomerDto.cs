using System.ComponentModel.DataAnnotations;

namespace BakeryManagement.Application.DTOs.Customers
{
    public class UpdateCustomerDto
    {
        [Required]
        [StringLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(254)]
        [EmailAddress]
        public required string Email { get; set; }

        [StringLength(32)]
        public string? PhoneNumber { get; set; }
    }
}
