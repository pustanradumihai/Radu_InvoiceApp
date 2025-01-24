using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadocInvoice.Models;

namespace RadocInvoice.Pages
{
    public class ClientsModel : PageModel
    {
        private readonly AppDbContext _context;

        public ClientsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Client> Clients { get; set; }

        public void OnGet()
        {
            Clients = _context.Clients.ToList();  // Retrieve all clients from the database
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Clients");  // Redirect back to the index page after deletion
        }
    }
}
