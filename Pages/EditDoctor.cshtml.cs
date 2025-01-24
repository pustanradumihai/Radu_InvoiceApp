using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadocInvoice.Models;

namespace RadocInvoice.Pages
{
    public class EditDoctorModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditDoctorModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Doctor Doctor { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Doctor = await _context.Doctors.FindAsync(id);

            if (Doctor == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var doctorToUpdate = await _context.Doctors.FindAsync(id);

            if (doctorToUpdate == null)
            {
                return NotFound();
            }

            doctorToUpdate.Name = Doctor.Name;
            doctorToUpdate.PracticeLicenseNumber = Doctor.PracticeLicenseNumber;
            doctorToUpdate.Email = Doctor.Email;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Doctors");
        }
    }
}
