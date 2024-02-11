using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal class Transaction
    {
        private static int nextTeansactionID = 0;
        public int TransactionID { get; private set; }
        public int UserID { get; private set;}
        public int BookID { get; private set;}
        public DateTime TransactionDate { get; private set;}
        public DateTime? ReturnDate { get; private set;}
        public void SetReturnDate(DateTime? returnDate)
        {
            ReturnDate = returnDate;
        }
        public Transaction(int userID, int bookID, DateTime transactionDate, DateTime? returnDate)
        {
            TransactionID = ++nextTeansactionID;
            UserID = userID;
            BookID = bookID;
            TransactionDate = transactionDate;
            ReturnDate = returnDate;
        }
        public void RecordTransaction(int userID, int bookID)
        {
            Transaction myTransaction = new Transaction(userID, bookID, DateTime.Now, null);
            Console.WriteLine($"Transaction ID: {TransactionID}");
        }
        public void DisplayTransactionDetails()
        {
            Console.WriteLine($"\nTransaction ID: {TransactionID}");
            Console.WriteLine($"User ID: {UserID}");
            Console.WriteLine($"Book ID: {BookID}");
            Console.WriteLine($"Transaction Date: {TransactionDate}");
            Console.WriteLine($"Return Date: {ReturnDate}\n");
        }

    }
}
