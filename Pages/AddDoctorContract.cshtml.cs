using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RadocInvoice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadocInvoice.Pages
{
    public class AddDoctorContractModel : PageModel
    {
        private readonly AppDbContext _context; //primim o instanța a bazei de date

        public AddDoctorContractModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DoctorContract DoctorContract { get; set; }

        public List<Doctor> Doctors { get; set; }  // For dropdown list

        public async Task<IActionResult> OnGetAsync()
        {
            // Load all doctors for the dropdown
            Doctors = await _context.Doctors.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload doctors in case of validation failure
                Doctors = await _context.Doctors.ToListAsync();
                return Page();
            }

            // Get the selected doctor's name for display purposes
            var selectedDoctor = await _context.Doctors.FindAsync(DoctorContract.DoctorId);
            if (selectedDoctor == null)
            {
                ModelState.AddModelError("DoctorContract.DoctorId", "Selected doctor does not exist.");
                Doctors = await _context.Doctors.ToListAsync();
                return Page();
            }

            // DoctorContract.Doctor.Name = selectedDoctor.Name;

            _context.DoctorContracts.Add(DoctorContract);
            await _context.SaveChangesAsync();

            return RedirectToPage("/DoctorContracts");
        }
    }

}
