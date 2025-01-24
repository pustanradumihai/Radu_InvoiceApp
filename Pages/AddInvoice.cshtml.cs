using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RadocInvoice.Models;

namespace RadocInvoice.Pages
{
    public class AddInvoiceModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddInvoiceModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Invoice Invoice { get; set; }

        public SelectList ClientList { get; set; }
        public SelectList ServiceList { get; set; }
        public SelectList DoctorList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ClientList = new SelectList(await _context.Clients.ToListAsync(), "Id", "Name");
            ServiceList = new SelectList(await _context.Services.ToListAsync(), "Id", "Name");
            DoctorList = new SelectList(await _context.Doctors.ToListAsync(), "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ClientList = new SelectList(await _context.Clients.ToListAsync(), "Id", "Name");
                ServiceList = new SelectList(await _context.Services.ToListAsync(), "Id", "Name");
                DoctorList = new SelectList(await _context.Doctors.ToListAsync(), "Id", "Name");
                return Page();
            }

            _context.Invoices.Add(Invoice);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Invoices");
        }
    }
}
