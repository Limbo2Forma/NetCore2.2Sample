using Microsoft.EntityFrameworkCore;
using System;

namespace Sample1.Models {
    public class BookContext : DbContext {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        public DbSet<Sample1.Models.Book> Books { get; set; }

        public DbSet<Sample1.Models.Author> Authors { get; set; }

        public void populateAuthors() {
            Authors.Add(new Author() { Name = "Ralls, Kim" });
            Authors.Add(new Author() { Name = "Corets, Eva" });
            Authors.Add(new Author() { Name = "Randall, Cynthia" });
            Authors.Add(new Author() { Name = "Thurman, Paula" });
            SaveChanges();
        }

        public void populateBooks() {
            Books.Add(new Book() {
                 Title = "Midnight Rain",
                 Genre = "Fantasy",
                 PublishDate = new DateTime(2000, 12, 16),
                 AuthorId = 1,
                 Description =
                    "A former architect battles an evil sorceress.",
                 Price = 14.95M
            });
            Books.Add(new Book() {
                Title = "Maeve Ascendant",
                Genre = "Fantasy",
                PublishDate = new DateTime(2000, 11, 17),
                AuthorId = 2,
                Description =
                "After the collapse of a nanotechnology society, the young" +
                "survivors lay the foundation for a new society.",
                Price = 12.95M
            });
            Books.Add(new Book() {
                Title = "The Sundered Grail",
                Genre = "Fantasy",
                PublishDate = new DateTime(2001, 09, 10),
                AuthorId = 2,
                Description =
                "The two daughters of Maeve battle for control of England.",
                Price = 12.95M
            });
            Books.Add(new Book() {
                Title = "Lover Birds",
                Genre = "Romance",
                PublishDate = new DateTime(2000, 09, 02),
                AuthorId = 3,
                Description =
                "When Carla meets Paul at an ornithology conference, tempers fly.",
                Price = 7.99M
            });
            Books.Add(new Book() {
                Title = "Splish Splash",
                Genre = "Romance",
                PublishDate = new DateTime(2000, 11, 02),
                AuthorId = 4,
                Description =
                "A deep sea diver finds true love 20,000 leagues beneath the sea.",
                Price = 6.99M
            });
            SaveChanges();
        }
    }
}
