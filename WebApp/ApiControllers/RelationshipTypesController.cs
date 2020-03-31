using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RelationshipTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelationshipType>>> GetRelationshipTypes()
        {
            return await _context.RelationshipTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RelationshipType>> GetRelationshipType(Guid id)
        {
            var relationshipType = await _context.RelationshipTypes.FindAsync(id);

            if (relationshipType == null)
            {
                return NotFound();
            }

            return relationshipType;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelationshipType(Guid id, RelationshipType relationshipType)
        {
            if (id != relationshipType.Id)
            {
                return BadRequest();
            }

            _context.Entry(relationshipType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationshipTypeExists(id))
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

        [HttpPost]
        public async Task<ActionResult<RelationshipType>> PostRelationshipType(RelationshipType relationshipType)
        {
            _context.RelationshipTypes.Add(relationshipType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelationshipType", new { id = relationshipType.Id }, relationshipType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RelationshipType>> DeleteRelationshipType(Guid id)
        {
            var relationshipType = await _context.RelationshipTypes.FindAsync(id);
            if (relationshipType == null)
            {
                return NotFound();
            }

            _context.RelationshipTypes.Remove(relationshipType);
            await _context.SaveChangesAsync();

            return relationshipType;
        }

        private bool RelationshipTypeExists(Guid id)
        {
            return _context.RelationshipTypes.Any(e => e.Id == id);
        }
    }
}
