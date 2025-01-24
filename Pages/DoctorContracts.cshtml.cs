using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RadocInvoice.Models;

namespace RadocInvoice.Pages
{
    public class DoctorContractsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DoctorContractsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<DoctorContract> DoctorContracts { get; set; }

        public async Task OnGetAsync()
        {
            DoctorContracts = await _context.DoctorContracts.ToListAsync();
            foreach (DoctorContract contract in DoctorContracts)
            {
                contract.Doctor = _context.Doctors.First(doc => doc.Id == contract.DoctorId);
            }
        }
    }
}
