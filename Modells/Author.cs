using System;

namespace Bibliotekssystem.Models
{
    public class BookAuthor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Book { get; set; }
    };
}
