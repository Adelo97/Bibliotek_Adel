using System.Collections.Generic;
using System.Linq;

namespace Bibliotekssystem.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public ICollection<BookAuthors> BookAuthors { get; set; }
        public ICollection<Loan> Loan { get; set; }

       
    }

   
}
