using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RadocInvoice.Models;

namespace RadocInvoice.Pages
{
    public class DeleteInvoiceModel : PageModel
    {
        private readonly AppDbContext _context;

        public Invoice Invoice { get; set; }

        public DeleteInvoiceModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Invoice = await _context.Invoices
                .Include(i => i.Service)
                .Include(i => i.Client)
                .Include(i => i.Doctor)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (Invoice == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Invoices");
        }
    }
}
