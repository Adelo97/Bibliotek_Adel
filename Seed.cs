using Bibliotekssystem.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Bibliotekssystem.Data;


public class Seed
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            if(!context.Books.Any())
            {
                var book1 = new Book { Title = "The Great Gatsby" };
                var book2 = new Book { Title = "To Kill a Mockingbird" };
                context.Books.AddRange(book1, book2);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("There is alrady book");
            }
            if (!context.Authors.Any())
            {
                var author1 = new Author { Name = "Forfatter 1" };
                var author2 = new Author { Name = "Forfatter 2" };
                context.Authors.AddRange(author1, author2);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("There is already author");
            }
            if (!context.BookAuthors.Any())
            {
                var book1 = context.Books.FirstOrDefault(b => b.Title == "The Great Gatsby");
                if (book1 == null)
                {
                    Console.WriteLine("The Great Gatsby not found");
                    return;
                }
                var author1 = context.Authors.FirstOrDefault(a => a.Name == "Jack Daniel");
                if (author1 == null)
                {
                    Console.WriteLine("Jack Daniel not found");
                    return;
                }
 
                var book2 = context.Books.FirstOrDefault(b => b.Title == "To Kill a Mockingbird");
                if (book2 == null)
                {
                    Console.WriteLine("To Kill a Mockingbird not found");
                    return;
                }

                var author2 = context.Authors.FirstOrDefault(a => a.Name == "Elon Musk");
                if (author2 == null)
                {
                    Console.WriteLine("Elon Musk not found");
                    return;
                }
 
                context.BookAuthors.Add(new BookAuthors { BookId = book1.BookId, AuthorId = author1.AuthorId });
                context.BookAuthors.Add(new BookAuthors { BookId = book2.BookId, AuthorId = author2.AuthorId });
 
                context.SaveChanges();
                Console.WriteLine("Book-author relationships have been added successfully.");
            }
            

            
        }
    }
}