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

        public string SearchQuery { get; set; }


        public void OnGet(string? searchQuery = null)
        {
            SearchQuery = searchQuery;

            if (!string.IsNullOrEmpty(SearchQuery))
            {
                // transf in lit 2 be sure
                Clients = _context.Clients
                    .Where(c => c.Name.ToLower().Contains(SearchQuery.ToLower()))
                    .OrderBy(c => c.Name)
                    .ToList();
            }
            else
            {
                // afișam tot da nu regasim cautarea
                Clients = _context.Clients
                    .OrderBy(c => c.Name)
                    .ToList();
            }
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

            return RedirectToPage("/Clients");  
        }
    }
}
