﻿using Microsoft.EntityFrameworkCore;

namespace Sample1.Models {
    public class BookContext : DbContext {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        public DbSet<Sample1.Models.Book> Books { get; set; }

        public DbSet<Sample1.Models.Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Book>()
                .Property(b => b.AuthorId)
                .HasDefaultValue((long)1);
        }
    }
}
