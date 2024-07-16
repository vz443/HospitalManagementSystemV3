using HospitalManagementSystemV3.App.Print;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemV3.App
{
    class AdminMenu : PrintText, IMenu
    {
        public AdminMenu(AppDbContext context, IUser loggedInUser)
        {

        }

        public void DisplayMainMenu()
        {
            Console.Clear();
            base.PrintHeader("Administrator Menu");

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

                Console.WriteLine($"{(option == 1 ? decorator : "   ")}1. List All Doctors\u001b[0m");
                Console.WriteLine($"{(option == 2 ? decorator : "   ")}2. Check Doctor Details\u001b[0m");
                Console.WriteLine($"{(option == 3 ? decorator : "   ")}3. List All Patients\u001b[0m");
                Console.WriteLine($"{(option == 4 ? decorator : "   ")}4. Check Patient Details\u001b[0m");
                Console.WriteLine($"{(option == 5 ? decorator : "   ")}5. Add Doctor\u001b[0m");
                Console.WriteLine($"{(option == 6 ? decorator : "   ")}6. Add Patient\u001b[0m");
                Console.WriteLine($"{(option == 7 ? decorator : "   ")}7. Logout\u001b[0m");
                Console.WriteLine($"{(option == 8 ? decorator : "   ")}8. Exit\u001b[0m");

                key = Console.ReadKey(false);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        option = option == 1 ? 8 : option - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        option = option == 8 ? 1 : option + 1;
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }

            switch (option)
            {
                case 1:
                    ListAllDoctors();
                    break;
                case 2:
                    CheckDoctorDetails();
                    break;
                case 3:
                    ListAllPatients();
                    break;
                case 4:
                    CheckPatientDetails();
                    break;
                case 5:
                    AddDoctor();
                    break;
                case 6:
                    AddPatient();
                    break;
                case 7:
                    //logout 
                    break;
                case 8:
                    Environment.Exit(0);
                    break;
            }
        }

        public void ListAllDoctors()
        {

        }

        public void CheckDoctorDetails()
        {

        }

        public void ListAllPatients()
        {

        }

        public void CheckPatientDetails()
        {

        }

        public void AddDoctor()
        {

        }

        public void AddPatient()
        {

        }
    }
}
