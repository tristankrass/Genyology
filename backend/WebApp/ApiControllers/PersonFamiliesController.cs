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
    public class PersonFamiliesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonFamiliesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonFamilies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonFamily>>> GetPersonFamilies()
        {
            return await _context.PersonFamilies.ToListAsync();
        }

        // GET: api/PersonFamilies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonFamily>> GetPersonFamily(Guid id)
        {
            var personFamily = await _context.PersonFamilies.FindAsync(id);

            if (personFamily == null)
            {
                return NotFound();
            }

            return personFamily;
        }

        // PUT: api/PersonFamilies/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonFamily(Guid id, PersonFamily personFamily)
        {
            if (id != personFamily.Id)
            {
                return BadRequest();
            }

            _context.Entry(personFamily).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonFamilyExists(id))
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

        // POST: api/PersonFamilies
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PersonFamily>> PostPersonFamily(PersonFamily personFamily)
        {
            _context.PersonFamilies.Add(personFamily);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonFamily", new { id = personFamily.Id }, personFamily);
        }

        // DELETE: api/PersonFamilies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonFamily>> DeletePersonFamily(Guid id)
        {
            var personFamily = await _context.PersonFamilies.FindAsync(id);
            if (personFamily == null)
            {
                return NotFound();
            }

            _context.PersonFamilies.Remove(personFamily);
            await _context.SaveChangesAsync();

            return personFamily;
        }

        private bool PersonFamilyExists(Guid id)
        {
            return _context.PersonFamilies.Any(e => e.Id == id);
        }
    }
}
