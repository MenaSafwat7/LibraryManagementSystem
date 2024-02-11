using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal class Users
    {
        private static int NextUserID = 0;
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Users(string name, string address, string phone, string email)
        {
            UserID = ++NextUserID;
            Name = name;
            Address = address;
            Phone = phone;
            Email = email;
        }
        public void DisplayUserInfo()
        {
            Console.WriteLine($"User ID = {UserID}");
            Console.WriteLine($"Name = {Name}");
            Console.WriteLine($"Address = {Address}");
            Console.WriteLine($"Phone = {Phone}");
            Console.WriteLine($"Email = {Email}");
        }
    }
}
