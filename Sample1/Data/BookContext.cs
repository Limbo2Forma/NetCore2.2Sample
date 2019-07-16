using Microsoft.EntityFrameworkCore;

namespace Sample1.Models {
    public class BookContext : DbContext {
        public BookContext (DbContextOptions<BookContext> options): base(options) { }

        public DbSet<Sample1.Models.Book> Books { get; set; }

        public DbSet<Sample1.Models.Author> Authors { get; set; }
    }
}
