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
    public class RelationshipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelationshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Relationships
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Relationships.Include(r => r.PersonOne).Include(r => r.PersonTwo).Include(r => r.RelationshipType).Include(r => r.RoleOne).Include(r => r.RoleTwo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Relationships/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationship = await _context.Relationships
                .Include(r => r.PersonOne)
                .Include(r => r.PersonTwo)
                .Include(r => r.RelationshipType)
                .Include(r => r.RoleOne)
                .Include(r => r.RoleTwo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relationship == null)
            {
                return NotFound();
            }

            return View(relationship);
        }

        // GET: Relationships/Create
        public IActionResult Create()
        {
            ViewData["PersonOneId"] = new SelectList(_context.Persons, "Id", "FirstName");
            ViewData["PersonTwoId"] = new SelectList(_context.Persons, "Id", "FirstName");
            ViewData["RelationshipTypeId"] = new SelectList(_context.RelationshipTypes, "Id", "RelationshipTypeName");
            ViewData["RoleOneId"] = new SelectList(_context.RelationshipRoles, "Id", "RelationshipRoleDescription");
            ViewData["RoleTwoId"] = new SelectList(_context.RelationshipRoles, "Id", "RelationshipRoleDescription");
            return View();
        }

        // POST: Relationships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RelationshipName,RelationShipDetails,DateTimeFrom,DateTimeTo,PersonOneId,PersonTwoId,RoleOneId,RoleTwoId,RelationshipTypeId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Relationship relationship)
        {
            if (ModelState.IsValid)
            {
                relationship.Id = Guid.NewGuid();
                _context.Add(relationship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonOneId"] = new SelectList(_context.Persons, "Id", "FirstName", relationship.PersonOneId);
            ViewData["PersonTwoId"] = new SelectList(_context.Persons, "Id", "FirstName", relationship.PersonTwoId);
            ViewData["RelationshipTypeId"] = new SelectList(_context.RelationshipTypes, "Id", "RelationshipTypeName", relationship.RelationshipTypeId);
            ViewData["RoleOneId"] = new SelectList(_context.RelationshipRoles, "Id", "RelationshipRoleDescription", relationship.RoleOneId);
            ViewData["RoleTwoId"] = new SelectList(_context.RelationshipRoles, "Id", "RelationshipRoleDescription", relationship.RoleTwoId);
            return View(relationship);
        }

        // GET: Relationships/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationship = await _context.Relationships.FindAsync(id);
            if (relationship == null)
            {
                return NotFound();
            }
            ViewData["PersonOneId"] = new SelectList(_context.Persons, "Id", "FirstName", relationship.PersonOneId);
            ViewData["PersonTwoId"] = new SelectList(_context.Persons, "Id", "FirstName", relationship.PersonTwoId);
            ViewData["RelationshipTypeId"] = new SelectList(_context.RelationshipTypes, "Id", "RelationshipTypeName", relationship.RelationshipTypeId);
            ViewData["RoleOneId"] = new SelectList(_context.RelationshipRoles, "Id", "RelationshipRoleDescription", relationship.RoleOneId);
            ViewData["RoleTwoId"] = new SelectList(_context.RelationshipRoles, "Id", "RelationshipRoleDescription", relationship.RoleTwoId);
            return View(relationship);
        }

        // POST: Relationships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RelationshipName,RelationShipDetails,DateTimeFrom,DateTimeTo,PersonOneId,PersonTwoId,RoleOneId,RoleTwoId,RelationshipTypeId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Relationship relationship)
        {
            if (id != relationship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relationship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelationshipExists(relationship.Id))
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
            ViewData["PersonOneId"] = new SelectList(_context.Persons, "Id", "FirstName", relationship.PersonOneId);
            ViewData["PersonTwoId"] = new SelectList(_context.Persons, "Id", "FirstName", relationship.PersonTwoId);
            ViewData["RelationshipTypeId"] = new SelectList(_context.RelationshipTypes, "Id", "RelationshipTypeName", relationship.RelationshipTypeId);
            ViewData["RoleOneId"] = new SelectList(_context.RelationshipRoles, "Id", "RelationshipRoleDescription", relationship.RoleOneId);
            ViewData["RoleTwoId"] = new SelectList(_context.RelationshipRoles, "Id", "RelationshipRoleDescription", relationship.RoleTwoId);
            return View(relationship);
        }

        // GET: Relationships/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationship = await _context.Relationships
                .Include(r => r.PersonOne)
                .Include(r => r.PersonTwo)
                .Include(r => r.RelationshipType)
                .Include(r => r.RoleOne)
                .Include(r => r.RoleTwo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relationship == null)
            {
                return NotFound();
            }

            return View(relationship);
        }

        // POST: Relationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var relationship = await _context.Relationships.FindAsync(id);
            _context.Relationships.Remove(relationship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelationshipExists(Guid id)
        {
            return _context.Relationships.Any(e => e.Id == id);
        }
    }
}
