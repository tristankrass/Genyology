using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Pages.RelationshipTypes
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public DetailsModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        public RelationshipType RelationshipType { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RelationshipType = await _context.RelationshipTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (RelationshipType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
