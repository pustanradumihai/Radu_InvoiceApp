using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadocInvoice.Models;

namespace RadocInvoice.Pages
{
    public class EditServiceModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditServiceModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Service Service { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Service = await _context.Services.FindAsync(id);

            if (Service == null)
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

            var serviceToUpdate = await _context.Services.FindAsync(id);

            if (serviceToUpdate == null)
            {
                return NotFound();
            }

            serviceToUpdate.Name = Service.Name;
            serviceToUpdate.Details = Service.Details;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Services");
        }
    }
}
