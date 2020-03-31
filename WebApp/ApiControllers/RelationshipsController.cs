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
    public class RelationshipsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RelationshipsController(ApplicationDbContext context)
        {
            _context = context;
        }


        private static RelationshipDTO MapRelationshipToRelationshipDto(Relationship relationship) => 
            new RelationshipDTO()
            {
                Id = relationship.Id,
                DateTimeFrom = relationship.DateTimeFrom,
                DateTimeTo = relationship.DateTimeTo,
                PersonOneId = relationship.PersonOneId,
                PersonTwoId = relationship.PersonTwoId,
                RelationshipName = relationship.RelationshipName,
                RelationShipDetails = relationship.RelationShipDetails,
                RelationshipTypeId = relationship.RelationshipTypeId,
                RoleOneId = relationship.RoleOneId,
                RoleTwoId = relationship.RoleTwoId

            };

        private static Relationship MapRelationshipDtoToRelationship(RelationshipDTO relationshipDTO)
        {
            var relationship = new Relationship(){
                Id = relationshipDTO.Id,
                DateTimeFrom = relationshipDTO.DateTimeFrom,
                DateTimeTo = relationshipDTO.DateTimeTo,
                PersonOneId = relationshipDTO.PersonOneId,
                PersonTwoId = relationshipDTO.PersonTwoId,
                RelationshipName = relationshipDTO.RelationshipName,
                RelationShipDetails = relationshipDTO.RelationShipDetails,
                RelationshipTypeId = relationshipDTO.RelationshipTypeId,
                RoleOneId = relationshipDTO.RoleOneId,
                RoleTwoId = relationshipDTO.RoleTwoId
            };

            if (relationshipDTO.Id.ToString().Length == 36)
            {
                relationship.Id = relationship.Id;
            }

            MetaData.AddMetaData(relationship);

            return relationship;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelationshipDTO>>> GetRelationships()
        {
            return Ok((await _context.Relationships.ToListAsync()).Select(MapRelationshipToRelationshipDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RelationshipDTO>> GetRelationship(Guid id)
        {
            var relationship = await _context.Relationships.FindAsync(id);

            if (relationship == null)
            {
                return NotFound();
            }

            return MapRelationshipToRelationshipDto(relationship);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelationship(Guid id, RelationshipDTO relationshipDTO)
        {
            if (id != relationshipDTO.Id)
            {
                return BadRequest();
            }

            var relationship = MapRelationshipDtoToRelationship(relationshipDTO);

            _context.Entry(relationship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationshipExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(relationshipDTO);
        }

        [HttpPost]
        public async Task<ActionResult<RelationshipDTO>> PostRelationship(RelationshipDTO relationshipDTO)
        {
            var relationship = MapRelationshipDtoToRelationship(relationshipDTO);

            _context.Relationships.Add(relationship);

            var id = await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelationship", new { id }, relationshipDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RelationshipDTO>> DeleteRelationship(Guid id)
        {
            var relationship = await _context.Relationships.FindAsync(id);
            if (relationship == null)
            {
                return NotFound();
            }

            _context.Relationships.Remove(relationship);
            await _context.SaveChangesAsync();

            return MapRelationshipToRelationshipDto(relationship);
        }

        private bool RelationshipExists(Guid id)
        {
            return _context.Relationships.Any(e => e.Id == id);
        }
    }
}
