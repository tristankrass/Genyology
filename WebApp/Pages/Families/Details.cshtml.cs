using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Pages.Families
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public DetailsModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        public Family Family { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Family = await _context.Families
                .Include(f => f.Person).FirstOrDefaultAsync(m => m.Id == id);

            if (Family == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
