using System;
using System.Security.Cryptography;
using Bibliotekssystem.Data;
using Bibliotekssystem.Models;
using Bibliotekssystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
 
namespace Bibliotekssystem
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to the library");
            Seed.Run();
            int menuSel = 0;
 
            do
            {
                menuSel = MenuSelection();
                MenuExecution(menuSel);
            } while (menuSel != 12);
        }
 
        static int MenuSelection()
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. View Books");
            Console.WriteLine("2. Create book");
            Console.WriteLine("3. Create author");
            Console.WriteLine("4. View Author");
            Console.WriteLine("5. Loan Book");
            Console.WriteLine("6. Relation");
            Console.WriteLine("7. Return");
            Console.WriteLine("8. Remove");
            Console.WriteLine("9. Update");
            Console.WriteLine("10. ListBook");
            Console.WriteLine("11. ListLoan");
            Console.WriteLine("12. Exit");
            Console.Write("Enter your choice: ");
 
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || (selection < 1 || selection > 12))
            {
                Console.Write("Invalid selection ");
            }
 
            return selection;
        }
 
        static void MenuExecution(int selection)
        {
            switch (selection)
            {
                case 1:
                    ViewBooks();
                    break;
                case 2:
                    CreateBook();
                    break;
 
                case 3:
                    CreateAuthor();
                    break;
 
                case 4:
                    ViewAuthor();
                    break;
 
                case 5:
                    LoanBook();
                    break;
                case 6:
                    Relation();
                    break;
 
                case 7:
                    ReturnLoan();
                    break;
 
                case 8:
                    RemoveBook();
                    break;
 
                case 9:
                    UpdateBook();
                    break;
                case 10:
                    ListBook();
                    break;
 
                case 11:
                    ListLoan();
                    break;
 
                case 12:
                    Console.WriteLine("Exiting the program. Thank you!");
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }
        }
 
        private static void ListLoan()
        {
            using (var context = new AppDbContext())
            {
                var loans = context.Loans.Include(l => l.Book).ToList(); // Removed IsReturned filter
 
                if (!loans.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("There are no loan books currently.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine("===============================================");
                    System.Console.WriteLine("List of Loans");
                    Console.WriteLine("===============================================");
 
                    foreach (var loan in loans)
                    {
                        System.Console.WriteLine("\n-----------------------------------------------");
                        System.Console.WriteLine($"Book Title: {loan.Book.Title}");
                        System.Console.WriteLine($"Borrower: {loan.Borrower}");
                        // Removed IsReturned field to display anymore
                        System.Console.WriteLine("-----------------------------------------------");
                    }
 
                    System.Console.WriteLine("\n===============================================");
                }
            }
        }
 
        private static void ListBook()
        {
            using (var context = new AppDbContext())
            {
                var books = context.Books.Include(b => b.BookAuthors)
                    .ThenInclude(BookAuthor => BookAuthor.author)
                    .ToList();
 
                foreach (var book in books)
                {
                    System.Console.WriteLine("===============================================");
                    System.Console.WriteLine($"Book ID: {book.BookId}");
                    System.Console.WriteLine($"Title: {book.Title}");
                    System.Console.WriteLine("===============================================");
 
                    foreach (var author in book.BookAuthors)
                    {
                        System.Console.WriteLine("\nAuthor Info:");
                        System.Console.WriteLine($"- Author ID: {author.author.AuthorId}");
                        System.Console.WriteLine($"- Name: {author.author.Name}");
                        System.Console.WriteLine("-----------------------------------------------");
                    }
                    Console.WriteLine("\n===============================================\n");
                }
            }
        }
 
        private static void UpdateBook()
        {
            using (var context = new AppDbContext())
            {
                var books = context.Books.ToList();
                if (!books.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("No book found");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    foreach (var boo in books) // List all books
                    {
                        System.Console.WriteLine($"ID: {boo.BookId} title: {boo.Title}");
                    }
                }
                System.Console.Write("Enter Book ID to update: ");
                if (!int.TryParse(Console.ReadLine(), out var bookId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Invalid Book ID.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
 
                var book = context.Books.Find(bookId);
                if (book == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Book not found.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
 
                System.Console.WriteLine($"Current Book title: {book.Title}");
                System.Console.WriteLine("Enter new title (leave blank to keep current): ");
                var title = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(title))
                {
                    book.Title = title;
                }
 
                context.SaveChanges();
                Console.WriteLine("Book updated successfully.");
            }
        }
 
        private static void RemoveBook()
        {
            using (var context = new AppDbContext())
            {
                var books = context.Books.ToList();
                if (!books.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("No book found");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    foreach (var boo in books) // List all books
                    {
                        System.Console.WriteLine($"ID: {boo.BookId} title: {boo.Title}");
                    }
                }
                System.Console.Write("Enter book id to remove: ");
                if (int.TryParse(Console.ReadLine(), out var bookId))
                {
                    var book = context.Books.Find(bookId);
                    if (book != null)
                    {
                        var bookauth = context.BookAuthors.Where(ba => ba.BookId == bookId).ToList();
                        context.BookAuthors.RemoveRange(bookauth);
 
                        context.Books.Remove(book);
                        context.SaveChanges();
                        Console.ForegroundColor = ConsoleColor.Green;
                        System.Console.WriteLine("Book removed successfully");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Book not found.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
 
            }
        }
 
        private static void ReturnLoan()
        {
            using (var context = new AppDbContext())
            {
                System.Console.Write("Enter Borrower's Name: ");
                var borrowerName = Console.ReadLine();
 
                Console.Write("Enter Book ID to return: ");
                if (!int.TryParse(Console.ReadLine(), out var bookId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Invalid Book ID.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
 
                var loan = context.Loans
                    .FirstOrDefault(l => l.Borrower == borrowerName && l.BookId == bookId);
 
                if (loan == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("No active loan found for this borrower and book.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
 
                loan.ReturnDate = DateTime.Now; // Removed IsReturned field
 
                var book = context.Books.Find(bookId);
 
                context.SaveChanges();
 
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"Book with ID {bookId} has been returned by {borrowerName}.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
 
        private static void Relation()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Enter Book ID: ");
                if (!int.TryParse(Console.ReadLine(), out var bookId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Book ID.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
 
                Console.Write("Enter Author ID: ");
                if (!int.TryParse(Console.ReadLine(), out var authorId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Author ID.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
 
                var bookAuthor = new BookAuthors
                {
                    BookId = bookId,
                    AuthorId = authorId
                };
 
                context.BookAuthors.Add(bookAuthor);
                context.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Relationship added between Book {bookId} and Author {authorId}.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
 
        private static void ViewAuthor()
        {
            using (var context = new AppDbContext())
            {
                var authors = context.Authors.ToList();
                if (!authors.Any())
                {
                    Console.WriteLine("No authors found.");
                }
                else
                {
                    Console.WriteLine("Authors:");
                    foreach (var author in authors)
                    {
                        Console.WriteLine($"{author.AuthorId}, name: {author.Name}");
                    }
                }
            }
        }
 
        private static void CreateAuthor()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Enter the author's name: ");
                var authorName = Console.ReadLine();
                var author1 = new Author { Name = authorName };
                context.Authors.Add(author1);
                context.SaveChanges();
                Console.WriteLine("Author has been created.");
            }
        }
 
        static void ViewBooks()
        {
            using (var context = new AppDbContext())
            {
                var books = context.Books.ToList();
                Console.WriteLine("Books:");
                if (!books.Any())
                {
                    Console.WriteLine("No books in the database");
                }
                else
                {
                    foreach (var book in books)
                    {
                        Console.WriteLine($"Title: {book.Title}");
                    }
                }
            }
        }
 
        static void CreateBook()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Enter the book's title: ");
                var bookTitle = Console.ReadLine();
                var book1 = new Book { Title = bookTitle };
                context.Books.Add(book1);
                context.SaveChanges();
            }
        }
 
        static void LoanBook()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Enter Borrower's Name: ");
                var borrower = Console.ReadLine();
                Console.Write("Enter Book ID: ");
                if (!int.TryParse(Console.ReadLine(), out var bookId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Book ID.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
 
                var book = context.Books.Find(bookId);
                if (book == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Book not found.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
 
                var loan = new Loan
                {
                    BookId = bookId,
                    Borrower = borrower,
                    LoanDate = DateTime.Now
                };
 
                context.Loans.Add(loan);
                context.SaveChanges();
                Console.WriteLine($"Loan added for Book {bookId}, borrowed by {borrower}.");
            }
        }
    }
}
 
 