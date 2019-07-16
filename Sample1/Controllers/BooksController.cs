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
    //[Authorize]
    [ApiController]
    public class BooksController : ControllerBase {
        private readonly BookContext _context;

        public BooksController(BookContext context) {
            _context = context;

            if (_context.Authors.Count() == 0) {
                _context.Authors.Add(new Author() { AuthorId = 1, Name = "Ralls, Kim" });
                _context.Authors.Add(new Author() { AuthorId = 2, Name = "Corets, Eva" });
                _context.Authors.Add(new Author() { AuthorId = 3, Name = "Randall, Cynthia" });
                _context.Authors.Add(new Author() { AuthorId = 4, Name = "Thurman, Paula" });
            }

            if (_context.Books.Count() == 0) {
                _context.Books.Add(new Book() {
                    BookId = 1,
                    Title = "Midnight Rain",
                    Genre = "Fantasy",
                    PublishDate = new DateTime(2000, 12, 16),
                    AuthorId = 1,
                    Description =
                    "A former architect battles an evil sorceress.",
                    Price = 14.95M
                });
                _context.Books.Add(new Book() {
                    BookId = 2,
                    Title = "Maeve Ascendant",
                    Genre = "Fantasy",
                    PublishDate = new DateTime(2000, 11, 17),
                    AuthorId = 2,
                    Description =
                    "After the collapse of a nanotechnology society, the young" +
                    "survivors lay the foundation for a new society.",
                    Price = 12.95M
                });
                _context.Books.Add(new Book() {
                    BookId = 3,
                    Title = "The Sundered Grail",
                    Genre = "Fantasy",
                    PublishDate = new DateTime(2001, 09, 10),
                    AuthorId = 2,
                    Description =
                    "The two daughters of Maeve battle for control of England.",
                    Price = 12.95M
                });
                _context.Books.Add(new Book() {
                    BookId = 4,
                    Title = "Lover Birds",
                    Genre = "Romance",
                    PublishDate = new DateTime(2000, 09, 02),
                    AuthorId = 3,
                    Description =
                    "When Carla meets Paul at an ornithology conference, tempers fly.",
                    Price = 7.99M
                });
                _context.Books.Add(new Book() {
                    BookId = 5,
                    Title = "Splish Splash",
                    Genre = "Romance",
                    PublishDate = new DateTime(2000, 11, 02),
                    AuthorId = 4,
                    Description =
                    "A deep sea diver finds true love 20,000 leagues beneath the sea.",
                    Price = 6.99M
                });
                _context.SaveChanges();
            }
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
            return await _context.Books.Include(b => b.Author)
                .Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
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
        public async Task<ActionResult<Book>> PostBook(Book book) {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id) {
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
