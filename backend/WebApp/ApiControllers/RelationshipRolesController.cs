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
    public class RelationshipRolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RelationshipRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RelationshipRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelationshipRole>>> GetRelationshipRoles()
        {
            return await _context.RelationshipRoles.ToListAsync();
        }

        // GET: api/RelationshipRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RelationshipRole>> GetRelationshipRole(Guid id)
        {
            var relationshipRole = await _context.RelationshipRoles.FindAsync(id);

            if (relationshipRole == null)
            {
                return NotFound();
            }

            return relationshipRole;
        }

        // PUT: api/RelationshipRoles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelationshipRole(Guid id, RelationshipRole relationshipRole)
        {
            if (id != relationshipRole.Id)
            {
                return BadRequest();
            }

            _context.Entry(relationshipRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationshipRoleExists(id))
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

        // POST: api/RelationshipRoles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<RelationshipRole>> PostRelationshipRole(RelationshipRole relationshipRole)
        {
            _context.RelationshipRoles.Add(relationshipRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelationshipRole", new { id = relationshipRole.Id }, relationshipRole);
        }

        // DELETE: api/RelationshipRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RelationshipRole>> DeleteRelationshipRole(Guid id)
        {
            var relationshipRole = await _context.RelationshipRoles.FindAsync(id);
            if (relationshipRole == null)
            {
                return NotFound();
            }

            _context.RelationshipRoles.Remove(relationshipRole);
            await _context.SaveChangesAsync();

            return relationshipRole;
        }

        private bool RelationshipRoleExists(Guid id)
        {
            return _context.RelationshipRoles.Any(e => e.Id == id);
        }
    }
}
