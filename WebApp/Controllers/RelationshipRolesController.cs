using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class RelationshipRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelationshipRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RelationshipRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.RelationshipRoles.ToListAsync());
        }

        // GET: RelationshipRoles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationshipRole = await _context.RelationshipRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relationshipRole == null)
            {
                return NotFound();
            }

            return View(relationshipRole);
        }

        // GET: RelationshipRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RelationshipRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RelationshipRoleDescription,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] RelationshipRole relationshipRole)
        {
            if (ModelState.IsValid)
            {
                relationshipRole.Id = Guid.NewGuid();
                _context.Add(relationshipRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(relationshipRole);
        }

        // GET: RelationshipRoles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationshipRole = await _context.RelationshipRoles.FindAsync(id);
            if (relationshipRole == null)
            {
                return NotFound();
            }
            return View(relationshipRole);
        }

        // POST: RelationshipRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RelationshipRoleDescription,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] RelationshipRole relationshipRole)
        {
            if (id != relationshipRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relationshipRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelationshipRoleExists(relationshipRole.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(relationshipRole);
        }

        // GET: RelationshipRoles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationshipRole = await _context.RelationshipRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relationshipRole == null)
            {
                return NotFound();
            }

            return View(relationshipRole);
        }

        // POST: RelationshipRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var relationshipRole = await _context.RelationshipRoles.FindAsync(id);
            _context.RelationshipRoles.Remove(relationshipRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelationshipRoleExists(Guid id)
        {
            return _context.RelationshipRoles.Any(e => e.Id == id);
        }
    }
}
