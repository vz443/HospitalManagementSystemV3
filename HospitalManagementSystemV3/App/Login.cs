using HospitalManagementSystemV3.App.Interface;
using HospitalManagementSystemV3.App.Print;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace HospitalManagementSystemV3.App
{
    class Login : PrintText, IMenu
    {
        public Login(AppDbContext context)
        {
            _context = context;
            PrintHeader("Login");
            DisplayMainMenu();
        }
        
        public bool IsLoggedIn { get; private set; }

        public IUser LoggedInUser { get; private set; }

        AppDbContext _context;

        public void DisplayMainMenu()
        {
            Console.Write("ID: ");
            var username = Console.ReadLine();

            string password = GetPassword();
              
            while (!ValidateLogin(username, password))
            {
                password = GetPassword();
                if (!ValidateLogin(username, password))
                {
                    Console.WriteLine("Invalid Credentials");
                }    
            }
            Console.WriteLine("Valid Credentials");

            IsLoggedIn = true;
        }

        public string GetPassword()
        {
            Console.Write("Password: ");
            string password = string.Empty;
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += (keyInfo.KeyChar);
                    Console.Write("*");
                }
                if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password = password[0..^1];
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            {
                return password;
            }
        }

        public bool ValidateLogin(string username, string password)
        {
            var doctorList = _context.Doctors.ToList();

            var patientList = _context.Patients.ToList();

             List<IUser> totalList =new List<IUser>();

            foreach (IUser user in patientList)
            {
                totalList.Add(user);
            }

            foreach (IUser user in doctorList)
            {
                totalList.Add(user);
            }

            foreach (var user in totalList)
            {
                if (user.Password == password && user.Username == username)
                {
                    LoggedInUser = user;
                    return true;
                }
            }

            return false;
        }
    }               
}
