using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace RadocInvoice.Models
{
    public class Client
    {
        public int Id { get; set; } // pk

        [Required]
        [MinLength(5, ErrorMessage = "Name is too short. Must be 5+ letters")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "Social security number must contain exactly 13 digits.")]
        public string SocialSecurityNumber { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must contain exactly 10 digits.")]
        public string? PhoneNumber { get; set; }  // optional...
    }
}
