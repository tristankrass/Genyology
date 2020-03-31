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
    public class PersonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private static PersonDto MapPersonToPersonDto(Person p) => new PersonDto()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                MiddleName = p.MiddleName,
                IdCode = p.IdCode,
                Sex = (int)p.Sex,
                DateOfBirth = p.DateOfBirth,
                PlaceOfBirth = p.PlaceOfBirth,
                AppUserId = p.AppUserId,
            };

        private static Person MapPersonDtoToPerson(PersonDto p)
        {
            var person = new Person()
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                MiddleName = p.MiddleName,
                IdCode = p.IdCode,
                Sex = (Sex)p.Sex,
                DateOfBirth = p.DateOfBirth,
                PlaceOfBirth = p.PlaceOfBirth,
                AppUserId = p.AppUserId,
                // Meta fields
                ChangedAt = DateTime.UtcNow,
                ChangedBy = p.AppUserId
            };

            if (p.Id.ToString().Length == 36)
            {
                person.Id = p.Id;
            }

            MetaData.AddMetaData(person);
            return person;
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetPersons(string? name)
        {
            var filter = name ?? "";
            return Ok((await _context.Persons.ToListAsync())

                .Where(p => p.FirstName.Contains(filter) ||
                                p.FirstName.StartsWith(filter) || p.FirstName.EndsWith(filter) ||
                                p.LastName.Contains(filter) ||
                                p.LastName.StartsWith(filter) || p.LastName.EndsWith(filter)
                )

                .Select(MapPersonToPersonDto));
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> GetPerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return MapPersonToPersonDto(person);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, PersonDto personDTO)
        {
            if (id != personDTO.Id)
            {
                return BadRequest();
            }
            
            var person = MapPersonDtoToPerson(personDTO);
           

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(personDTO);
        }

        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(PersonDto personDTO)
        {
            var person = MapPersonDtoToPerson(personDTO);

            _context.Persons.Add(person);
            
            var id = await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id }, personDTO);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonDto>> DeletePerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(Guid id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}
