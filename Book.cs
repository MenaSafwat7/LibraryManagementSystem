using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal class Book
    {
        private static int NextBookID = 0;
        public int BookID { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Genre { get; private set; }
        public bool Availability { get; private set; }
        public void SetAvailability(bool available)
        {
            Availability = available;
        }
        public Book(string title, string author, string genre, bool availability) 
        {
            BookID = ++NextBookID;
            Title = title;
            Author = author;
            Genre = genre;
            Availability = availability;
        }
        public void DisplayBookDetails()
        {
            Console.WriteLine($"Book id = {BookID}");
            Console.WriteLine($"Title = {Title}");
            Console.WriteLine($"Author = {Author}");
            Console.WriteLine($"Genre: {Genre}");
            Console.WriteLine($"Availability: {(Availability ? "Available" : "Not Available")}");
        }

    }
}
