using System.Collections.Generic;
using System.Linq;

namespace Bibliotekssystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }

    public class Author
    {
        public object Book { get; set; }
    }
}
