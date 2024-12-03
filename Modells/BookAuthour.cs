using System.Collections.Generic;
using System.Linq;

namespace Bibliotekssystem.Models
{
    public class BookAuthors
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book book { get; set; }
        public int AuthorId { get; set; }
        public Author author { get; set; }
    }
}