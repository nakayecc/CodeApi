using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoolApi.Models;
using CoolApi.Models.DtoModels;

namespace CoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookContext _context;

        public AuthorsController(BookContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<List<AuthorDTO>> GetAuthors()
        {

            var dbAuthors = await _context.Authors.Include(book => book.Books).ToListAsync();
             
            var authors = dbAuthors.Select(author => new AuthorDTO()
            {
                Id = author.Id,
                Name = author.Name,
                Books = author.Books.Select(b => new SimpleBookDTO()
                {
                    Id = b.Id, ISBN = b.ISBN, Name = b.Name, ReleaseDateTime = b.ReleaseDateTime
                })
            });



            return authors.ToList();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            var dbAuthors = _context.Authors;

            var authors =await _context.Authors.Include(book => book.Books).Select(author => new AuthorDTO()
            {
                Id = author.Id,
                Name = author.Name,
                Books = author.Books.Select(b => new SimpleBookDTO()
                {
                    Id = b.Id,
                    ISBN = b.ISBN,
                    Name = b.Name,
                    ReleaseDateTime = b.ReleaseDateTime
                })
            }).SingleOrDefaultAsync(author => author.Id == id);

            if (authors == null)
            {
                return NotFound();
            }

            return authors;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Author>> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return author;
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
