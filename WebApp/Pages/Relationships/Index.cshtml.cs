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
    public class IndexModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public IndexModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Relationship> Relationship { get;set; }

        public async Task OnGetAsync()
        {
            Relationship = await _context.Relationships
                .Include(r => r.Family)
                .Include(r => r.PersonOne)
                .Include(r => r.PersonTwo)
                .Include(r => r.RelationshipType)
                .Include(r => r.RoleOne)
                .Include(r => r.RoleTwo).ToListAsync();
        }
    }
}
