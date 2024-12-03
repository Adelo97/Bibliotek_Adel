using System;
using System.Security.Cryptography;
using Bibliotekssystem.Data;
using Bibliotekssystem.Models;
using BibliotekSystem;
using Microsoft.EntityFrameworkCore;


namespace BibliotekSystem
{
    class Program
    {
        static void Main(string[] args)
            {
            System.Console.WriteLine("\n Welcome to the library");
            Seed.Run();
            int menuSel = 0;

                do
                {
                    menuSel = MenuSelection();
                    MenuExecution(menuSel);
                } while (menuSel != 2);
            }

                static int MenuSelection()
                {
                    Console.WriteLine("\nPlease select an option:");
                    Console.WriteLine("1. View Books");
                    Console.WriteLine("2. Exit");
                    Console.Write("Enter your choice (1 or 2): ");

                    int selection;
                    while (!int.TryParse(Console.ReadLine(), out selection) || (selection < 1 || selection > 2))
                    {
                        Console.Write("Invalid selection. Please enter 1 or 2: ");
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
                            Console.WriteLine("Exiting the program. Thank you!");
                            break;
                        default:
                            Console.WriteLine("Invalid selection.");
                            break;
                    }
                }

                static void ViewBooks()
                {
                    // Here you would normally fetch and display the list of books from your database.
                    Console.WriteLine("Displaying the list of books:");
                    // Example book list (replace with actual data fetching)
                    Console.WriteLine("1. The Great Gatsby");
                    Console.WriteLine("2. To Kill a Mockingbird");
                    // Add more books as needed
                }
           
    }
}

