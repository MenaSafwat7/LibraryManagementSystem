using System.Transactions;
using System.Text.RegularExpressions;
using System.Numerics;

namespace ConsoleApp6
{
    internal class Program
    {

        static void Main(string[] args)
        {
            List<Book> BookList = new List<Book>();
            List<Users> UserList = new List<Users>();
            List<Transaction> TransactionList = new List<Transaction>();
            bool ExitProgram = false;
            do
            {
                Console.WriteLine("1) Add a new book");
                Console.WriteLine("2) Borrow a book");
                Console.WriteLine("3) Return a book");
                Console.WriteLine("4) Check book availability");
                Console.WriteLine("5) Display user information");
                Console.WriteLine("6) Display transaction history");
                Console.WriteLine("7) Exit");
                string? choice = Console.ReadLine();
                if (int.TryParse(choice, out int menuChoice)) {
                    switch (menuChoice)
                    {
                        case 1:
                            AddNewBook(BookList);
                            break;
                        case 2:
                            BorrowBook(BookList, UserList, TransactionList); 
                            break;
                        case 3:
                            returnBook(UserList, BookList, TransactionList);
                            break;
                        case 4:
                            Console.Write("Enter Book ID: ");
                            int bookid = Convert.ToInt32(Console.ReadLine());
                            if (CheckBookAvailability(BookList, bookid))
                                Console.WriteLine("YES");
                            else
                                Console.WriteLine("NO");
                            break;
                        case 5:
                            Console.Write("Enter your ID: ");
                            int userid = Convert.ToInt32(Console.ReadLine());
                            UserList[userid - 1].DisplayUserInfo();
                            break;
                        case 6:
                            foreach (Transaction transaction in TransactionList)
                            {
                                transaction.DisplayTransactionDetails();
                            }
                            break;
                        case 7:
                            ExitProgram = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number from 1 to 7.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            } while (!ExitProgram);
            
        }
        private static void AddNewBook(List<Book> bookList)
        {
            Console.Write("Enter Book title: ");
            string title = Console.ReadLine();
            string reg = @"^[a-zA-Z\s]+$";
            string author;
            do
            {
                Console.Write("Enter Book Author: ");
                author = Console.ReadLine();
                if(!Regex.IsMatch(author, reg))
                {
                    Console.WriteLine("Invalid name. Please enter a valid name (letters only).");
                }
            } while (!Regex.IsMatch(author, reg));
            
            Console.WriteLine("Choose Book Genre: ");
            string[] genreArr = { "Fiction", "Non - Fiction", "Mystery / Thriller", "Science Fiction / Fantasy", "Romance", "Children's and Young Adult" };
            int genreCount = 0;
            foreach (string genre in genreArr)
            {
                Console.WriteLine($"{++genreCount}) {genre}");
            }
            genreCount = Convert.ToInt32(Console.ReadLine());
            Book NewBook = new Book(title, author, genreArr[genreCount - 1], true);
            bookList.Add(NewBook);
            Console.WriteLine("Done");
        }
        private static void BorrowBook(List<Book> bookList, List<Users> userList, List<Transaction> transactionList)
        {
            string chooseRegex = @"^(1|2)$";
            string res;
            do
            {
                Console.WriteLine("\n1) new user");
                Console.WriteLine("2) old user");
                res = Console.ReadLine();
                if (!Regex.IsMatch(res, chooseRegex))
                {
                    Console.WriteLine("Invalid input. Please enter a number (1 or 2).");
                }
            } while (!Regex.IsMatch(res, chooseRegex));
            
            if (res == "2")
            {
                Login(userList, bookList, transactionList);
            }
            else if (res == "1")
            {
                AddNewUser(userList);
                Login(userList, bookList, transactionList);
            }
        }
        private static bool IsUserExists(int userId, List<Users> userList)
        {
            foreach (Users user in userList)
            {
                if (user.UserID == userId)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool IsValidPhoneNum(string phoneNum)
        {
            string PhoneRegex = @"^(012|010|011|015)\d{8}$";
            return Regex.IsMatch(phoneNum, PhoneRegex);
        }
        private static bool IsvalidEmail(string email)
        {
            string EmailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, EmailRegex);
        }
        private static void MkTransaction(List<Book> bookList, List<Transaction> transactionList, int userid)
        {
            foreach (Book book in bookList)
            {
                if (book.Availability)
                {
                    Console.WriteLine($"Book id: {book.BookID}  name: {book.Title}");
                }
            }
            Console.WriteLine("what's Book id do you want?");
            string idRegex = @"^\d+$";
            string bookid;
            bool f = false;
            do
            {
                if (f)
                {
                    Console.WriteLine("\nID must be numbers only*");
                    Console.Write("Enter Book ID: ");
                }
                bookid = Console.ReadLine();
                f = true;
            } while (!Regex.IsMatch(bookid, idRegex));
            Transaction newTransaction = new Transaction(userid, int.Parse(bookid), DateTime.Now, null);
            newTransaction.RecordTransaction(userid, int.Parse(bookid));
            transactionList.Add(newTransaction);
            bookList[int.Parse(bookid) - 1].SetAvailability(false);
        }
        private static void AddNewUser(List<Users> userList)
        {
            string Reg = @"^[a-zA-Z\s]+$";
            Console.WriteLine("Sign up");
            
            string name;
            do
            {
                Console.Write("Your name: ");
                name = Console.ReadLine();
                if (!Regex.IsMatch(name, Reg))
                {
                    Console.WriteLine("Invalid name. Please enter a valid name (Letters only).");
                }
            } while (!Regex.IsMatch(name, Reg));

            string address;
            do
            {
                Console.Write("Your address: ");
                address = Console.ReadLine();
                if (!Regex.IsMatch(address, Reg))
                {
                    Console.WriteLine("Invalid address. Please enter a valid address (Letters only).");
                }
            } while (!Regex.IsMatch(address, Reg));

            string phone;
            do
            {
                Console.Write("Phone: ");
                phone = Console.ReadLine();
                if (!IsValidPhoneNum(phone))
                {
                    Console.WriteLine("Invalid phone number. Phone numbre should start with (012,010,011,015).");
                }
            } while (!IsValidPhoneNum(phone));
            string email;
            
            do
            {
                Console.Write("Email: ");
                email = Console.ReadLine();
                if (!IsvalidEmail(email))
                {
                    Console.WriteLine("Invalid Email. Please enter a valid Email.");
                }
            } while(!IsvalidEmail(email));
            Users newUser = new Users(name, address, phone, email);
            Console.WriteLine($"Your ID: {newUser.UserID}");
            userList.Add(newUser);
        }
        private static void returnBook(List<Users> userList, List<Book> bookList, List<Transaction> transactionList)
        {
            Console.Write("Enter Your ID: ");
            int userid = Convert.ToInt32(Console.ReadLine());
            if (IsUserExists(userid, userList))
            {
                Console.WriteLine($"Hello {userList[userid - 1].Name}");
                foreach (Book book in bookList)
                {
                    if (!book.Availability)
                    {
                        Console.WriteLine($"Book id: {book.BookID}  name: {book.Title}");
                    }
                }
                Console.Write("Choose the book: ");
                int bookID = Convert.ToInt32(Console.ReadLine());
                bookList[bookID - 1].SetAvailability(true);
                foreach (Transaction transaction in transactionList)
                {
                    if (transaction.BookID == bookID)
                    {
                        transaction.SetReturnDate(DateTime.Now);
                        break;
                    }
                }
            }
        }
        private static bool CheckBookAvailability(List<Book> bookList, int bookID)
        {
            return bookList[bookID - 1].Availability;
        }
        private static void Login(List<Users> userList, List<Book> bookList, List<Transaction> transactionList)
        {
            string idRegex = @"^\d+$";
            string userid;
            Console.WriteLine("-----Login-----");
            Console.Write("Enter your ID: ");
            bool f = false;
            do
            {
                if (f)
                {
                    Console.WriteLine("\nID must be numbers only*");
                    Console.Write("Enter your ID: ");
                }
                userid = Console.ReadLine();
                f = true;
            } while (!Regex.IsMatch(userid, idRegex));
            if (IsUserExists(int.Parse(userid), userList))
            {
                MkTransaction(bookList, transactionList, int.Parse(userid));
            }
            else
            {
                Console.WriteLine("***ID Not Found***\n");
            }
        }
    }
}