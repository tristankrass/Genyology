using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Pages.Relationships
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public DetailsModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
