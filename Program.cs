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
            System.Console.WriteLine("System");
            Seed.Run();
            
        }
     }

}
