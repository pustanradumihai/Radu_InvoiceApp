using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace RadocInvoice.Models
{
    public class Doctor
    {
        public int Id { get; set; } // Primary key

        [Required]
        [MinLength(5, ErrorMessage = "The provided name is too short.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "License number can only contain capital letters and numbers.")]
        [MaxLength(12, ErrorMessage = "License number cannot exceed 12 characters.")]
        public string PracticeLicenseNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }  // Optional Email
    }
}
