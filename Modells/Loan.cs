using System;

namespace Bibliotekssystem.Models
{
    public class Loan
    {
        public int LoanId {get; set;}
        public int BookId {get; set;}
        public Book Book {get; set;}
        public string Borrower  {get; set;}
        public DateTime LoanDate {get; set;}
        public DateTime? ReturnDate {get; set;}
    }
}
