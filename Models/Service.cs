using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace RadocInvoice.Models
{
    public class Service
    {
        public int Id { get; set; } // Primary key

        [Required]
        [MinLength(5, ErrorMessage = "The provided name is too short.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Insufficient service details.")]
        public string Details { get; set; }
    }
}
