using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class FamilyRelationshipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FamilyRelationshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FamilyRelationships
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FamilyRelationships.Include(f => f.Family).Include(f => f.Relationship);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FamilyRelationships/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyRelationship = await _context.FamilyRelationships
                .Include(f => f.Family)
                .Include(f => f.Relationship)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (familyRelationship == null)
            {
                return NotFound();
            }

            return View(familyRelationship);
        }

        // GET: FamilyRelationships/Create
        public IActionResult Create()
        {
            ViewData["FamilyId"] = new SelectList(_context.Families, "Id", "FamilyName");
            ViewData["RelationshipId"] = new SelectList(_context.Relationships, "Id", "Id");
            return View();
        }

        // POST: FamilyRelationships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RelationshipId,FamilyId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] FamilyRelationship familyRelationship)
        {
            if (ModelState.IsValid)
            {
                familyRelationship.Id = Guid.NewGuid();
                _context.Add(familyRelationship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilyId"] = new SelectList(_context.Families, "Id", "FamilyName", familyRelationship.FamilyId);
            ViewData["RelationshipId"] = new SelectList(_context.Relationships, "Id", "Id", familyRelationship.RelationshipId);
            return View(familyRelationship);
        }

        // GET: FamilyRelationships/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyRelationship = await _context.FamilyRelationships.FindAsync(id);
            if (familyRelationship == null)
            {
                return NotFound();
            }
            ViewData["FamilyId"] = new SelectList(_context.Families, "Id", "FamilyName", familyRelationship.FamilyId);
            ViewData["RelationshipId"] = new SelectList(_context.Relationships, "Id", "Id", familyRelationship.RelationshipId);
            return View(familyRelationship);
        }

        // POST: FamilyRelationships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RelationshipId,FamilyId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] FamilyRelationship familyRelationship)
        {
            if (id != familyRelationship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(familyRelationship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyRelationshipExists(familyRelationship.Id))
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
            ViewData["FamilyId"] = new SelectList(_context.Families, "Id", "FamilyName", familyRelationship.FamilyId);
            ViewData["RelationshipId"] = new SelectList(_context.Relationships, "Id", "Id", familyRelationship.RelationshipId);
            return View(familyRelationship);
        }

        // GET: FamilyRelationships/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var familyRelationship = await _context.FamilyRelationships
                .Include(f => f.Family)
                .Include(f => f.Relationship)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (familyRelationship == null)
            {
                return NotFound();
            }

            return View(familyRelationship);
        }

        // POST: FamilyRelationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var familyRelationship = await _context.FamilyRelationships.FindAsync(id);
            _context.FamilyRelationships.Remove(familyRelationship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyRelationshipExists(Guid id)
        {
            return _context.FamilyRelationships.Any(e => e.Id == id);
        }
    }
}
