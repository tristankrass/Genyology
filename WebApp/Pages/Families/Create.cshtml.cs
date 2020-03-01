using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.App.EF;
using Domain;

namespace WebApp.Pages.Families
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
        ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName");
            return Page();
        }

        [BindProperty]
        public Family Family { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Families.Add(Family);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
