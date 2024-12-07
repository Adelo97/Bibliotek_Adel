using System;
using System.Security.Cryptography;
using Bibliotekssystem.Data;
using Bibliotekssystem.Models;
using BibliotekSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;


namespace BibliotekSystem
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
                } while (menuSel != 5);
            }

                static int MenuSelection()
                {
                    Console.WriteLine("\nPlease select an option:");
                    Console.WriteLine("1. View Books");
                    Console.WriteLine("2. creat book");
                    Console.WriteLine("3. creat author");
                    Console.WriteLine("4. view Author");
                    Console.WriteLine("5. Exit");
                    Console.Write("Enter your choice (1, 2, 3, 4 or 5): ");

                    int selection;
                    while (!int.TryParse(Console.ReadLine(), out selection) || (selection < 1 || selection > 5 ))
                    {
                        Console.Write("Invalid selection. Please enter 1, 2, 3, 4 or 5: ");
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
                            Console.WriteLine("Exiting the program. Thank you!");
                            break;
                        default:
                            Console.WriteLine("Invalid selection.");
                            break;
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
        }   }

        private static void CreateAuthor()
        {
            using (var context = new AppDbContext())
            {
                Console.Write("Enter the author's  name: ");
                var authorName = Console.ReadLine();
                var author1 = new Author { Name = authorName};
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
                        if(!books.Any())
                        {
                            Console.WriteLine("No books in the database");


                        }
                        else {
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
                    var book1 = new Book { Title = bookTitle};
                    context.Books.Add(book1);
                    context.SaveChanges();


                    }
                }


                
    }
}

