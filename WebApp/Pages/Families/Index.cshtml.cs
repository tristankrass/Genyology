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
    public class IndexModel : PageModel
    {
        private readonly DAL.App.EF.ApplicationDbContext _context;

        public IndexModel(DAL.App.EF.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Family> Family { get;set; }

        public async Task OnGetAsync()
        {
            Family = await _context.Families
                .Include(f => f.Person).ToListAsync();
        }
    }
}
