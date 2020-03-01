using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.App.EF;
using Domain;

namespace WebApp.Pages.Relationships
{
    public class CreateModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public CreateModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FamilyId"] = new SelectList(_context.Families, "Id", "FamilyName");
        ViewData["PersonOneId"] = new SelectList(_context.Persons, "Id", "FirstName");
        ViewData["PersonTwoId"] = new SelectList(_context.Persons, "Id", "FirstName");
        ViewData["RelationshipTypeId"] = new SelectList(_context.RelationshipTypes, "Id", "RelationShipTypeDescription");
        ViewData["RoleOneId"] = new SelectList(_context.Roles, "Id", "RoleDescription");
        ViewData["RoleTwoId"] = new SelectList(_context.Roles, "Id", "RoleDescription");
            return Page();
        }

        [BindProperty]
        public Relationship Relationship { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Relationships.Add(Relationship);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
