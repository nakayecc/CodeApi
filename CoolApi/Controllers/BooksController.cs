using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoolApi.Model;
using CoolApi.Models;
using CoolApi.Models.DtoModels;

namespace CoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookContext _context;

        public BooksController(BookContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<List<BookDTO>> GetBooks()
        {
            var dbBooks = _context.Books;

            var books = from b in dbBooks
                select new BookDTO()
                {
                    Id = b.Id,
                    ISBN = b.ISBN,
                    Name = b.Name,
                    ReleaseDateTime = b.ReleaseDateTime,
                    AuthorId = b.Author.Id
                };

            return await books.ToListAsync();


        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var dbBooks = _context.Books;

            var book = await _context.Books.Include(b => b.Author).Select(b => new BookDTO()
            {
                Id = b.Id,
                ISBN = b.ISBN,
                Name = b.Name,
                ReleaseDateTime = b.ReleaseDateTime,
                AuthorId = b.Author.Id
            }).SingleOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookDTO bookDto)
        {

            if (id != bookDto.Id)
            {
                return BadRequest();
            }

            if (_context.Authors.Find(bookDto.AuthorId) == null)
            {
                return BadRequest();
            }

            var book = new Book()
            {
                Id = bookDto.Id,
                ISBN = bookDto.ISBN,
                Name = bookDto.Name,
                ReleaseDateTime = bookDto.ReleaseDateTime,
                Author = _context.Authors.Find(bookDto.AuthorId)
            };

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookDTO bookDto)
        {

            /*var author = _context.Authors.Include(aut => aut.Books).First(a => a.Id == bookDto.AuthorId);*/
            var author = _context.Authors.Find(bookDto.AuthorId);

            var book = new Book()
            {
                //Id = bookDto.Id,
                ISBN = bookDto.ISBN,
                Name = bookDto.Name,
                ReleaseDateTime = bookDto.ReleaseDateTime,
                Author = author
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new {id = book.Id}, bookDto);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}