using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RadocInvoice.Models
{
    public class DoctorContract
    {
        public int Id { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }  //fk

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Contract Expiration Date")]
        public DateTime ContractExpirationDate { get; set; }

        [ValidateNever]
        public Doctor Doctor { get; set; } // prop. pt navigare
    }
}
