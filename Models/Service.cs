using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace RadocInvoice.Models
{
    public class Service
    {
        public int Id { get; set; } // pk

        [Required]
        [MinLength(5, ErrorMessage = "Name is too short. Must be 5+ letters")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Insufficient service details. Must be 5+ letters")]
        public string Details { get; set; }
    }
}
