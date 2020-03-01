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
    public class DeleteModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public DeleteModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RelationshipType = await _context.RelationshipTypes.FindAsync(id);

            if (RelationshipType != null)
            {
                _context.RelationshipTypes.Remove(RelationshipType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
