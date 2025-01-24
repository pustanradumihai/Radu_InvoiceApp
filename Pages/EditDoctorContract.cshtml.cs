using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RadocInvoice.Models;

namespace RadocInvoice.Pages
{
    public class EditDoctorContractModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditDoctorContractModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DoctorContract DoctorContract { get; set; }

        public SelectList DoctorList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DoctorContract = await _context.DoctorContracts.FindAsync(id);
            if (DoctorContract == null)
            {
                return NotFound();
            }

            var doctors = await _context.Doctors.ToListAsync();
            DoctorList = new SelectList(doctors, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var doctors = await _context.Doctors.ToListAsync();
                DoctorList = new SelectList(doctors, "Id", "Name");
                return Page();
            }

            var contractToUpdate = await _context.DoctorContracts.FindAsync(DoctorContract.Id);
            if (contractToUpdate == null)
            {
                return NotFound();
            }

            contractToUpdate.DoctorId = DoctorContract.DoctorId;
            contractToUpdate.ContractExpirationDate = DoctorContract.ContractExpirationDate;

            await _context.SaveChangesAsync();

            return RedirectToPage("/DoctorContracts");
        }
    }
}
