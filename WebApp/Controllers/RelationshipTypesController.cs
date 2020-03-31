using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class RelationshipTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelationshipTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RelationshipTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.RelationshipTypes.ToListAsync());
        }

        // GET: RelationshipTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationshipType = await _context.RelationshipTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relationshipType == null)
            {
                return NotFound();
            }

            return View(relationshipType);
        }

        // GET: RelationshipTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RelationshipTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RelationshipTypeName,RelationShipTypeDescription,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] RelationshipType relationshipType)
        {
            if (ModelState.IsValid)
            {
                relationshipType.Id = Guid.NewGuid();
                _context.Add(relationshipType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(relationshipType);
        }

        // GET: RelationshipTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationshipType = await _context.RelationshipTypes.FindAsync(id);
            if (relationshipType == null)
            {
                return NotFound();
            }
            return View(relationshipType);
        }

        // POST: RelationshipTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RelationshipTypeName,RelationShipTypeDescription,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] RelationshipType relationshipType)
        {
            if (id != relationshipType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relationshipType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelationshipTypeExists(relationshipType.Id))
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
            return View(relationshipType);
        }

        // GET: RelationshipTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationshipType = await _context.RelationshipTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relationshipType == null)
            {
                return NotFound();
            }

            return View(relationshipType);
        }

        // POST: RelationshipTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var relationshipType = await _context.RelationshipTypes.FindAsync(id);
            _context.RelationshipTypes.Remove(relationshipType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelationshipTypeExists(Guid id)
        {
            return _context.RelationshipTypes.Any(e => e.Id == id);
        }
    }
}
