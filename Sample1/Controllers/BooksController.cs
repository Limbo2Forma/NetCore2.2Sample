using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample1.Models;
using Sample1.Models.DTOs;

namespace Sample1.Controllers {
    [Route("api/books")]
    [Authorize]
    [ApiController]
    public class BooksController : ControllerBase {
        private readonly BookContext _context;

        public BooksController(BookContext context) {
            _context = context;
        }

        private static readonly Expression<Func<Book, BookDTO>> AsBookDTO =
            x => new BookDTO {
                Title = x.Title,
                Author = x.Author.Name,
                Genre = x.Genre
            };

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks() {
            return await _context.Books.Include(b => b.Author).Select(AsBookDTO).ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult<BookDTO>> GetBook(long id) {
            var book = await _context.Books.Include(b => b.Author)
                .Where(b => b.BookId == id)
                .Select(AsBookDTO)
                .FirstOrDefaultAsync();
            if (book == null) {
                return NotFound();
            }

            return book;
        }

        // GET: api/Books/5/detail
        [HttpGet("{id:long}/details")]
        public async Task<ActionResult<BookDetailDTO>> GetBookDetail(long id) {
            var book = await (from b in _context.Books.Include(b => b.Author)
                              where b.BookId == id
                              select new BookDetailDTO {
                                  Title = b.Title,
                                  Genre = b.Genre,
                                  PublishDate = b.PublishDate,
                                  Price = b.Price,
                                  Description = b.Description,
                                  Author = b.Author.Name
                              }).FirstOrDefaultAsync();
            if (book == null) {
                return NotFound();
            }

            return book;
        }

        // GET: api/Books/Fantasy
        [HttpGet("{genre}")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksByGenre(string genre) {
            return await _context.Books.FromSql($"GetBooksByGenre {genre}")
                .Select(AsBookDTO).ToListAsync();
        }

        //GET: api/Books/date/2000-09-02
        //GET: api/Books/date/2000/09/02
        [HttpGet("date/{pubdate:datetime:regex(\\d{{4}}-\\d{{2}}-\\d{{2}})}")]
        [HttpGet("date/{*pubDate:datetime:regex(\\d{{4}}/\\d{{2}}/\\d{{2}})}")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksByDate(DateTime pubDate) {
            Console.WriteLine(pubDate);
            return await _context.Books.Include(b => b.Author)
                .Where(b => b.PublishDate == pubDate)
                .Select(AsBookDTO).ToListAsync();
        }

        //GET: api/author/1/books
        [HttpGet("~/api/authors/{authorId}/books")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksByAuthor(long authorId) {
            return await _context.Books.Include(b => b.Author)
                .Where(b => b.AuthorId == authorId)
                .Select(AsBookDTO).ToListAsync();
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book) {
            if (id != book.BookId) {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!BookExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookDetailDTO>> PostBook([FromBody] Book book) {

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(long id) {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        private bool BookExists(int id) {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
