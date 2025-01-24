using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadocInvoice.Models;

namespace RadocInvoice.Pages
{
    public class EditClientModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditClientModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Client Client { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Client = await _context.Clients.FindAsync(id);

            if (Client == null)
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

            var clientToUpdate = await _context.Clients.FindAsync(id);

            if (clientToUpdate == null)
            {
                return NotFound();
            }

            clientToUpdate.Name = Client.Name;
            clientToUpdate.SocialSecurityNumber = Client.SocialSecurityNumber;
            clientToUpdate.PhoneNumber = Client.PhoneNumber;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Clients");
        }
    }
}
