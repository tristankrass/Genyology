using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class PersonFamiliesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonFamiliesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonFamilies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PersonFamilies.Include(p => p.Family).Include(p => p.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PersonFamilies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personFamily = await _context.PersonFamilies
                .Include(p => p.Family)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personFamily == null)
            {
                return NotFound();
            }

            return View(personFamily);
        }

        // GET: PersonFamilies/Create
        public IActionResult Create()
        {
            ViewData["FamilyId"] = new SelectList(_context.Families, "Id", "FamilyName");
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName");
            return View();
        }

        // POST: PersonFamilies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FamilyId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PersonFamily personFamily)
        {
            if (ModelState.IsValid)
            {
                personFamily.Id = Guid.NewGuid();
                _context.Add(personFamily);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FamilyId"] = new SelectList(_context.Families, "Id", "FamilyName", personFamily.FamilyId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", personFamily.PersonId);
            return View(personFamily);
        }

        // GET: PersonFamilies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personFamily = await _context.PersonFamilies.FindAsync(id);
            if (personFamily == null)
            {
                return NotFound();
            }
            ViewData["FamilyId"] = new SelectList(_context.Families, "Id", "FamilyName", personFamily.FamilyId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", personFamily.PersonId);
            return View(personFamily);
        }

        // POST: PersonFamilies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PersonId,FamilyId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PersonFamily personFamily)
        {
            if (id != personFamily.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personFamily);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonFamilyExists(personFamily.Id))
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
            ViewData["FamilyId"] = new SelectList(_context.Families, "Id", "FamilyName", personFamily.FamilyId);
            ViewData["PersonId"] = new SelectList(_context.Persons, "Id", "FirstName", personFamily.PersonId);
            return View(personFamily);
        }

        // GET: PersonFamilies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personFamily = await _context.PersonFamilies
                .Include(p => p.Family)
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personFamily == null)
            {
                return NotFound();
            }

            return View(personFamily);
        }

        // POST: PersonFamilies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var personFamily = await _context.PersonFamilies.FindAsync(id);
            _context.PersonFamilies.Remove(personFamily);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonFamilyExists(Guid id)
        {
            return _context.PersonFamilies.Any(e => e.Id == id);
        }
    }
}
