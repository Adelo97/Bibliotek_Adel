using Microsoft.EntityFrameworkCore;
using Bibliotekssystem.Models;

namespace Bibliotekssystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<BookAuthors> BookAuthors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ADEL;Database=BibliotekssystemDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the composite key for BookAuthors
            modelBuilder.Entity<BookAuthors>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            // Configure relationships for BookAuthors
            modelBuilder.Entity<BookAuthors>()
                .HasOne(ba => ba.book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthors>()
                .HasOne(ba => ba.author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            // Configure Loan relationship with Book
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book)
                .WithMany(b => b.Loan) // Ensure this matches the navigation property in Book
                .HasForeignKey(l => l.BookId);
        }
    }
}
