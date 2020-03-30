using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyRelationshipsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FamilyRelationshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FamilyRelationships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilyRelationship>>> GetFamilyRelationships()
        {
            return await _context.FamilyRelationships.ToListAsync();
        }

        // GET: api/FamilyRelationships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyRelationship>> GetFamilyRelationship(Guid id)
        {
            var familyRelationship = await _context.FamilyRelationships.FindAsync(id);

            if (familyRelationship == null)
            {
                return NotFound();
            }

            return familyRelationship;
        }

        // PUT: api/FamilyRelationships/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFamilyRelationship(Guid id, FamilyRelationship familyRelationship)
        {
            if (id != familyRelationship.Id)
            {
                return BadRequest();
            }

            _context.Entry(familyRelationship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyRelationshipExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FamilyRelationships
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<FamilyRelationship>> PostFamilyRelationship(FamilyRelationship familyRelationship)
        {
            _context.FamilyRelationships.Add(familyRelationship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFamilyRelationship", new { id = familyRelationship.Id }, familyRelationship);
        }

        // DELETE: api/FamilyRelationships/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FamilyRelationship>> DeleteFamilyRelationship(Guid id)
        {
            var familyRelationship = await _context.FamilyRelationships.FindAsync(id);
            if (familyRelationship == null)
            {
                return NotFound();
            }

            _context.FamilyRelationships.Remove(familyRelationship);
            await _context.SaveChangesAsync();

            return familyRelationship;
        }

        private bool FamilyRelationshipExists(Guid id)
        {
            return _context.FamilyRelationships.Any(e => e.Id == id);
        }
    }
}
