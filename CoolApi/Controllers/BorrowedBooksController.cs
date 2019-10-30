using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoolApi.Models;

namespace CoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedBooksController : ControllerBase
    {
        private readonly BookContext _context;

        public BorrowedBooksController(BookContext context)
        {
            _context = context;
        }

        // GET: api/BorrowedBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowedBooks>>> GetBorrowedBookses()
        {
            return await _context.BorrowedBookses
                .Include(user => user.User)
                .Include(book => book.Book)
                .ToListAsync();
        }

        // GET: api/BorrowedBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowedBooks>> GetBorrowedBooks(int id)
        {
            var borrowedBooks = await _context.BorrowedBookses.FindAsync(id);

            if (borrowedBooks == null)
            {
                return NotFound();
            }

            return borrowedBooks;
        }

        // PUT: api/BorrowedBooks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrowedBooks(int id, BorrowedBooks borrowedBooks)
        {
            if (id != borrowedBooks.Id)
            {
                return BadRequest();
            }

            _context.Entry(borrowedBooks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowedBooksExists(id))
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

        // POST: api/BorrowedBooks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BorrowedBooks>> PostBorrowedBooks(BorrowedBooks borrowedBooks)
        {
            _context.BorrowedBookses.Add(borrowedBooks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBorrowedBooks", new { id = borrowedBooks.Id }, borrowedBooks);
        }

        // DELETE: api/BorrowedBooks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BorrowedBooks>> DeleteBorrowedBooks(int id)
        {
            var borrowedBooks = await _context.BorrowedBookses.FindAsync(id);
            if (borrowedBooks == null)
            {
                return NotFound();
            }

            _context.BorrowedBookses.Remove(borrowedBooks);
            await _context.SaveChangesAsync();

            return borrowedBooks;
        }

        private bool BorrowedBooksExists(int id)
        {
            return _context.BorrowedBookses.Any(e => e.Id == id);
        }
    }
}
