using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadocInvoice.Models;

namespace RadocInvoice.Pages
{
    public class AddClientModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddClientModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Client Client { get; set; }

        public void OnGet()
        {
            // Initialize Client if needed
            Client = new Client();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Clients.Add(Client);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Clients");
        }
    }
}
