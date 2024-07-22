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
            while (true)
            {
                Console.Clear();
                PrintText.PrintHeader("Login");

                Console.Write("ID: ");
                var username = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Username cannot be empty. Please try again.");
                    continue;
                }

                string password = GetPassword();

                while (true)
                {
                    if (ValidateLogin(username, password))
                    {
                        Console.WriteLine("\nValid Credentials");
                        IsLoggedIn = true;
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        PrintText.PrintHeader("Login");
                        Console.WriteLine("Invalid Credentials");
                        Console.WriteLine("1. Try again with the same username");
                        Console.WriteLine("2. Enter a new username");
                        Console.Write("Choice: ");
                        var choice = Console.ReadLine();

                        if (choice == "1")
                        {
                            Console.Clear();
                            PrintText.PrintHeader("Login");
                            password = GetPassword();
                        }
                        else if (choice == "2")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Please try again.");
                        }
                    }
                }
            }
        }

        public string GetPassword()
        {
            Console.Write("Password: ");
            string password = string.Empty;
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                keyInfo = Console.ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password = password[0..^1];
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return password;
                }
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
