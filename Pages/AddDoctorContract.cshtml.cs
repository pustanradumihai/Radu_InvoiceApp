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
        private readonly AppDbContext _context; 

        public AddDoctorContractModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DoctorContract DoctorContract { get; set; }

        public List<Doctor> Doctors { get; set; }  // 4dd 

        public async Task<IActionResult> OnGetAsync()
        {
            // afisam doc in dd list
            Doctors = await _context.Doctors.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                
                Doctors = await _context.Doctors.ToListAsync();
                return Page();
            }

            // afisam doc selectat
            var selectedDoctor = await _context.Doctors.FindAsync(DoctorContract.DoctorId);
            if (selectedDoctor == null)
            {
                ModelState.AddModelError("DoctorContract.DoctorId", "Selected doctor does not exist.");
                Doctors = await _context.Doctors.ToListAsync();
                return Page();
            }

          

            _context.DoctorContracts.Add(DoctorContract);
            await _context.SaveChangesAsync();

            return RedirectToPage("/DoctorContracts");
        }
    }

}
