using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadocInvoice.Models;

namespace RadocInvoice.Pages
{
    public class DoctorsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DoctorsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Doctor> Doctors { get; set; }

        public void OnGet()
        {
            Doctors = _context.Doctors.ToList();  // Retrieve all doctors from the database
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Doctors");  // Redirect back to the index page after deletion
        }
    }
}
