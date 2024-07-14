using HospitalManagementSystemV3.App.Print;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Interface;
using HospitalManagementSystemV3.Models;
using Microsoft.Extensions.DependencyModel.Resolution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemV3.App
{
    class DoctorMenu : PrintText, IMenu
    {
        public DoctorMenu(AppDbContext context, IUser loggedInUser)
        {
            Console.Clear();

            _context = context;

            Doctor = loggedInUser;

            Startup();
        }

        AppDbContext _context;

        IUser Doctor;

        void Startup()
        {
            base.PrintHeader("Doctor Menu");

            DisplayMainMenu();
        }

        public void DisplayMainMenu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Cyan;
            (int left, int top) = Console.GetCursorPosition();
            var option = 1;
            var decorator = "✅ \u001b[32m";
            ConsoleKeyInfo key;
            bool isSelected = false;

            while (!isSelected)
            {
                Console.SetCursorPosition(left, top);

                Console.WriteLine($"{(option == 1 ? decorator : "   ")}1. List Doctor Details\u001b[0m");
                Console.WriteLine($"{(option == 2 ? decorator : "   ")}2. List Patients\u001b[0m");
                Console.WriteLine($"{(option == 3 ? decorator : "   ")}3. List Appointments\u001b[0m");
                Console.WriteLine($"{(option == 4 ? decorator : "   ")}4. Check particular patient\u001b[0m");
                Console.WriteLine($"{(option == 5 ? decorator : "   ")}5. List appointments with patient\u001b[0m");
                Console.WriteLine($"{(option == 6 ? decorator : "   ")}6. Logout\u001b[0m");
                Console.WriteLine($"{(option == 7 ? decorator : "   ")}7. Exit\u001b[0m");

                key = Console.ReadKey(false);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        option = option == 1 ? 7 : option - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        option = option == 7 ? 1 : option + 1;
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }

            switch (option)
            {
                case 1:
                    ListDoctorDetails();
                    break;
                case 2:
                    //ListPatients();
                    break;
                case 3:
                    //ListAppointments();
                    break;
                case 4:
                    //CheckParticularPatient();
                    break;
                case 5:
                    //ListAppointmentsWithPatient();
                    break;
                case 6:
                    //Logout();
                    break;
                case 7:
                    //Exit();
                    break;
            }    
        }

        public void ListDoctorDetails()
        {
            Console.Clear();
            base.PrintHeader("My Details");
            Console.WriteLine("Name            | Email Address   | Phone        | Address");
            Console.WriteLine($"{Doctor.Name,-16}| {Doctor.Email,-18}| {Doctor.Phone,-14}| {Doctor.Address}"); //make this line up 
        }
    }
}
