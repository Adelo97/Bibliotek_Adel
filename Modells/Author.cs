using System;

namespace Bibliotekssystem.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        //public ICollection<Book> Book { get; set; }
        public ICollection<BookAuthors> BookAuthors { get; set; }
    };
}
