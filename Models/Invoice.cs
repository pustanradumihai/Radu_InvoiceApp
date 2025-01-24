using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RadocInvoice.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Service")]
        public int ServiceId { get; set; }

        [ValidateNever]
        public Service Service { get; set; } // Navigation property

        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }

        [ValidateNever]
        public Client Client { get; set; } // Navigation property

        [Required]
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }

        [ValidateNever]
        public Doctor Doctor { get; set; } // Navigation property

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Cannot charge less than 1 or more than 10000.")]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
