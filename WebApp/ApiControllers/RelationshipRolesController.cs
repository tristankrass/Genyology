using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using PublicApi.DTO.V1;

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

        private static RelationshipRoleDTO MapRelationshipRoleToRelationshipRoleDto(RelationshipRole r) => new RelationshipRoleDTO()
        {
            Id = r.Id,
            RelationshipRoleName = r.RelationshipRoleName,
            RelationshipRoleDescription = r.RelationshipRoleDescription
        };

        private static RelationshipRole MapRelationshipRoleDTOToRelationshipRole(RelationshipRoleDTO r)
        {
            var relationship = new RelationshipRole()
            {
                RelationshipRoleName = r.RelationshipRoleName,
                RelationshipRoleDescription = r.RelationshipRoleDescription
            };

            if (r.Id.ToString().Length == 36)
            {
                relationship.Id = r.Id;
            }

            MetaData.AddMetaData(relationship);

            return relationship;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelationshipRoleDTO>>> GetRelationshipRoles()
        {
            return Ok((await _context.RelationshipRoles.ToListAsync()).Select(MapRelationshipRoleToRelationshipRoleDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RelationshipRoleDTO>> GetRelationshipRole(Guid id)
        {
            var relationshipRole = await _context.RelationshipRoles.FindAsync(id);

            if (relationshipRole == null)
            {
                return NotFound();
            }

            return MapRelationshipRoleToRelationshipRoleDto(relationshipRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelationshipRole(Guid id, RelationshipRoleDTO relationshipRoleDTO)
        {
            if (id != relationshipRoleDTO.Id)
            {
                return BadRequest();
            }

            var relationshipRole = MapRelationshipRoleDTOToRelationshipRole(relationshipRoleDTO);

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

            return Ok(relationshipRoleDTO);
        }

    
        [HttpPost]
        public async Task<ActionResult<RelationshipRole>> PostRelationshipRole(RelationshipRoleDTO relationshipRoleDTO)
        {
            var relationshipRole = MapRelationshipRoleDTOToRelationshipRole(relationshipRoleDTO);

            _context.RelationshipRoles.Add(relationshipRole);
           
            var id = await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelationshipRole", new { id }, relationshipRoleDTO);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<RelationshipRoleDTO>> DeleteRelationshipRole(Guid id)
        {
            var relationshipRole = await _context.RelationshipRoles.FindAsync(id);
            if (relationshipRole == null)
            {
                return NotFound();
            }

            _context.RelationshipRoles.Remove(relationshipRole);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RelationshipRoleExists(Guid id)
        {
            return _context.RelationshipRoles.Any(e => e.Id == id);
        }
    }
}
