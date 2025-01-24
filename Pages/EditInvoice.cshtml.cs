using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RadocInvoice.Models;

namespace RadocInvoice.Pages
{
    public class EditInvoiceModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public Invoice Invoice { get; set; }

        public SelectList ServicesList { get; set; }
        public SelectList ClientsList { get; set; }
        public SelectList DoctorsList { get; set; }

        public EditInvoiceModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Invoice = await _context.Invoices.FindAsync(id);

            if (Invoice == null)
            {
                return NotFound();
            }

            await LoadDropdownListsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownListsAsync();
                return Page();
            }

            var invoiceToUpdate = await _context.Invoices.FindAsync(Invoice.Id);
            if (invoiceToUpdate == null)
            {
                return NotFound();
            }

            invoiceToUpdate.Quantity = Invoice.Quantity;
            invoiceToUpdate.ServiceId = Invoice.ServiceId;
            invoiceToUpdate.ClientId = Invoice.ClientId;
            invoiceToUpdate.DoctorId = Invoice.DoctorId;
            invoiceToUpdate.Price = Invoice.Price;
            invoiceToUpdate.Date = Invoice.Date;

            await _context.SaveChangesAsync();
            return RedirectToPage("/Invoices");
        }

        private async Task LoadDropdownListsAsync()
        {
            var services = await _context.Services.ToListAsync();
            var clients = await _context.Clients.ToListAsync();
            var doctors = await _context.Doctors.ToListAsync();

            ServicesList = new SelectList(services, "Id", "Name");
            ClientsList = new SelectList(clients, "Id", "Name");
            DoctorsList = new SelectList(doctors, "Id", "Name");
        }
    }
}
