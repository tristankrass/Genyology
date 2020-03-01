using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Pages.RelationshipTypes
{
    public class EditModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public EditModel(DAL.App.EF.ApplicationDbContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RelationshipType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationshipTypeExists(RelationshipType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RelationshipTypeExists(Guid id)
        {
            return _context.RelationshipTypes.Any(e => e.Id == id);
        }
    }
}
