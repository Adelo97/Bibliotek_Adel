using System;
using System.Security.Cryptography;
using Bibliotekssystem.Data;
using Bibliotekssystem.Models;
using BibliotekSystem;


namespace BibliotekSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            (using var context = new DataContext())
            {
            
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var author1 = new Author { Name = "Författare 1" };
                var author2 = new Author { Name = "Författare 2" };

                var book1 = new Book { Title = "Bok 1" };
                var book2 = new Book { Title = "Bok 2" };

                context.Authors.AddRange(author1, author2);
                context.Books.AddRange(book1, book2);
                context.SaveChanges();

                context.BookAuthors.Add(new BookAuthor { BookId = book1.Id, AuthorId = author1.Id });
                context.BookAuthors.Add(new BookAuthor { BookId = book2.Id, AuthorId = author2.Id });
                context.Loans.Add(new Loan { BookId = book1.Id, LoanDate = DateTime.Now });
                context.SaveChanges();

                var books = context.Books
                    .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                    .ToList();

                foreach (var book in books)
                {
                    Console.WriteLine($"Bok: {book.Title}");
                    foreach (var author in book.BookAuthors.Select(ba => ba.Author))
                    {
                        Console.WriteLine($" - Författare: {author.Name}");
                    }
                }
            }
            
        }
    }
}
