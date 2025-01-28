using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadocInvoice.Models;
using System.Linq;

namespace RadocInvoice.Pages
{
    public class ServicesModel : PageModel
    {
        private readonly AppDbContext _context;

        public ServicesModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Service> Services { get; set; }

        public void OnGet()
        {
            // Retrieve all services from the database and sort them alphabetically by name
            Services = _context.Services
                .OrderBy(s => s.Name) // Sort alphabetically by the "Name" property
                .ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Services");  // Redirect back to the index page after deletion
        }
    }
}
