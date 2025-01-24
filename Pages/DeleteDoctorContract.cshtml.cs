using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadocInvoice.Models;

namespace RadocInvoice.Pages
{
    public class DeleteDoctorContractModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteDoctorContractModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DoctorContract DoctorContract { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DoctorContract = await _context.DoctorContracts.FindAsync(id);
            if (DoctorContract == null)
            {
                return NotFound();
            }
            DoctorContract.Doctor = await _context.Doctors.FindAsync(DoctorContract.DoctorId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var contract = await _context.DoctorContracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            _context.DoctorContracts.Remove(contract);
            await _context.SaveChangesAsync();

            return RedirectToPage("/DoctorContracts");
        }
    }
}
