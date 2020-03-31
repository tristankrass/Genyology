using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
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

        private static PersonDTO MapPersonToPersonDto(Person p)
        {
            return new PersonDTO()
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
        }
        private static Person MapPersonDtoToPerson(PersonDTO p)
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
            return person;
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetPersons()
        {
            return Ok((await _context.Persons.ToListAsync()).Select(MapPersonToPersonDto));
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> GetPerson(Guid id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return MapPersonToPersonDto(person);
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(Guid id, PersonDTO personDTO)
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
        public async Task<ActionResult<Person>> PostPerson(PersonDTO personDTO)
        {
            var person = MapPersonDtoToPerson(personDTO);

            _context.Persons.Add(person);
            
            var id = await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id }, personDTO);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonDTO>> DeletePerson(Guid id)
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
