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
        public Service Service { get; set; } // 4nav

        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }

        [ValidateNever]
        public Client Client { get; set; } // 4nav

        [Required]
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }

        [ValidateNever]
        public Doctor Doctor { get; set; } // 4nav

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [Range(1, 20000, ErrorMessage = "Cannot charge less than 1 or more than 20000.")]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
