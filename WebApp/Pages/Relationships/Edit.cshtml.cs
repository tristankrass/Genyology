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

namespace WebApp.Pages.Relationships
{
    public class EditModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public EditModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Relationship Relationship { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Relationship = await _context.Relationships
                .Include(r => r.Family)
                .Include(r => r.PersonOne)
                .Include(r => r.PersonTwo)
                .Include(r => r.RelationshipType)
                .Include(r => r.RoleOne)
                .Include(r => r.RoleTwo).FirstOrDefaultAsync(m => m.Id == id);

            if (Relationship == null)
            {
                return NotFound();
            }
           ViewData["FamilyId"] = new SelectList(_context.Families, "Id", "FamilyName");
           ViewData["PersonOneId"] = new SelectList(_context.Persons, "Id", "FirstName");
           ViewData["PersonTwoId"] = new SelectList(_context.Persons, "Id", "FirstName");
           ViewData["RelationshipTypeId"] = new SelectList(_context.RelationshipTypes, "Id", "RelationShipTypeDescription");
           ViewData["RoleOneId"] = new SelectList(_context.Roles, "Id", "RoleDescription");
           ViewData["RoleTwoId"] = new SelectList(_context.Roles, "Id", "RoleDescription");
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

            _context.Attach(Relationship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationshipExists(Relationship.Id))
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

        private bool RelationshipExists(Guid id)
        {
            return _context.Relationships.Any(e => e.Id == id);
        }
    }
}
