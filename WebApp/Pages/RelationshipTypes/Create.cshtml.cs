using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.App.EF;
using Domain;

namespace WebApp.Pages.RelationshipTypes
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
            return Page();
        }

        [BindProperty]
        public RelationshipType RelationshipType { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RelationshipTypes.Add(RelationshipType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
