using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Models;

namespace MyFirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly MyFirstAPIDBContext _context;

        public EmailController(MyFirstAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Email
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Email>>> GetEmails()
        {
          if (_context.Emails == null)
          {
              return NotFound();
          }
            return await _context.Emails.ToListAsync();
        }

        // GET: api/Email/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Email>> GetEmail(int id)
        {
          if (_context.Emails == null)
          {
              return NotFound();
          }
            var email = await _context.Emails.FindAsync(id);

            if (email == null)
            {
                return NotFound();
            }

            return email;
        }

        // PUT: api/Email/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmail(int id, Email email)
        {
            if (id != email.EmailId)
            {
                return BadRequest();
            }

            _context.Entry(email).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailExists(id))
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

        // POST: api/Email
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Email>> PostEmail(Email email)
        {
          if (_context.Emails == null)
          {
              return Problem("Entity set 'MyFirstAPIDBContext.Emails'  is null.");
          }
            _context.Emails.Add(email);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmail", new { id = email.EmailId }, email);
        }

        // DELETE: api/Email/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            if (_context.Emails == null)
            {
                return NotFound();
            }
            var email = await _context.Emails.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }

            _context.Emails.Remove(email);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmailExists(int id)
        {
            return (_context.Emails?.Any(e => e.EmailId == id)).GetValueOrDefault();
        }
    }
}
