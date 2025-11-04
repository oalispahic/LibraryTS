using System;
using System.Collections.Generic;
using System.Linq;

public class posudba
    {   public int LoanId { get; set; }
        public int userId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public double Fine { get; set; }

        public bool IsReturned => ReturnDate.HasValue;

        public override string ToString()
        {
            return $"[Posudba #{LoanId}] Korisnik: {userId}, Knjiga: {BookId}, " +
                   $"Posuđeno: {BorrowDate:d}, Rok: {DueDate:d}, " +
                   $"Vraćeno: {(ReturnDate.HasValue ? ReturnDate.Value.ToShortDateString() : "Nije vraćeno")}, " +
                   $"Kazna: {Fine:0.00} KM";
        }
    }

public class sistemposudbe
    {
        private List<posudba> loans = new List<posudba>();
        private int nextLoanId = 1;

        public void BorrowBook(int userId, int bookId)
        {
            if (loans.Any(l => l.userId == userId && l.BookId == bookId && !l.IsReturned))
            {
                Console.WriteLine("Knjiga je već posuđena od strane ovog korisnika!");
                return;
            }

            var newLoan = new posudba
            {
                LoanId = nextLoanId++,
                userId = userId,
                BookId = bookId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                Fine = 0
            };

            loans.Add(newLoan);
            Console.WriteLine($"Knjiga (ID {bookId}) uspješno posuđena korisniku (ID {userId}). Rok vraćanja: {newLoan.DueDate:d}");
        }

        public void ReturnBook(int userId, int bookId)
        {
            var loan = loans.FirstOrDefault(l => l.UserId == userId && l.BookId == bookId && !l.IsReturned);
            if (loan == null)
            {
                Console.WriteLine("Nema aktivne posudbe za ovu knjigu.");
                return;
            }

            loan.ReturnDate = DateTime.Now;

            if (loan.ReturnDate > loan.DueDate)
            {
                int daysLate = (loan.ReturnDate.Value - loan.DueDate).Days;
                loan.Fine = daysLate * 0.50; 
                Console.WriteLine($"Knjiga vraćena sa {daysLate} dana zakašnjenja. Kazna: {loan.Fine:0.00} KM");
            }
            else
            {
                Console.WriteLine("Knjiga vraćena na vrijeme.");
            }
        }

        public void ShowAllLoans()
        {
            Console.WriteLine("\nTrenutne posudbe:");
            foreach (var l in loans)
            {
                Console.WriteLine(l);
            }
        }

        public void ShowUserHistory(int userId)
        {
            var userLoans = loans.Where(l => l.UserId == userId).ToList();
            if (userLoans.Count == 0)
            {
                Console.WriteLine("Nema posudbi za ovog korisnika.");
                return;
            }

            Console.WriteLine($"\nHistorija posudbi za korisnika ID {userId}:");
            foreach (var loan in userLoans)
                Console.WriteLine(loan);
        }

        public void ShowActiveLoans()
        {
            var active = loans.Where(l => !l.IsReturned).ToList();
            if (active.Count == 0)
            {
                Console.WriteLine("Nema aktivnih posudbi.");
                return;
            }

            Console.WriteLine("\nAktivne posudbe:");
            foreach (var l in active)
                Console.WriteLine(l);
        }
    }
