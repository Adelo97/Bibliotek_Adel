using System.Collections.Generic;
using System.Linq;

namespace Bibliotekssystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
