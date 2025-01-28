using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadocInvoice.Models;
using System.Linq;

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
      
            Doctors = _context.Doctors
                .OrderBy(d => d.Name) // sort a-z
                .ToList();
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

            return RedirectToPage("/Doctors");  
        }
    }
}
