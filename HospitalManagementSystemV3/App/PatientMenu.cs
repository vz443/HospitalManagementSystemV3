using HospitalManagementSystemV3.App.Print;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Interface;
using HospitalManagementSystemV3.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace HospitalManagementSystemV3.App
{
    internal class PatientMenu : PrintText, IMenu
    {
        public PatientMenu(AppDbContext context, IUser loggedInUser)
        {
            Console.Clear();

            _context = context;

            currentPatient = loggedInUser;

            Startup();
        }

        AppDbContext _context;

        IUser currentPatient;

        public void Startup()
        {
            DisplayMainMenu();
        }

        public void DisplayMainMenu()
        {
            Console.Clear();
            base.PrintHeader("Patient Menu");
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

                Console.WriteLine($"{(option == 1 ? decorator : "   ")}1. List Patient Details\u001b[0m");
                Console.WriteLine($"{(option == 2 ? decorator : "   ")}2. List My Doctor Details\u001b[0m");
                Console.WriteLine($"{(option == 3 ? decorator : "   ")}3. List All Appointments\u001b[0m");
                Console.WriteLine($"{(option == 4 ? decorator : "   ")}4. Book Appointment\u001b[0m");
                Console.WriteLine($"{(option == 5 ? decorator : "   ")}5. Exit To Login\u001b[0m");
                Console.WriteLine($"{(option == 6 ? decorator : "   ")}6. Exit System\u001b[0m");

                key = Console.ReadKey(false);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        option = option == 1 ? 6 : option - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        option = option == 6 ? 1 : option + 1;
                        break;

                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
            }

            switch (option)
            {
                case 1:
                    ListPatientDetails();
                    break;
                case 2:
                    ListDoctorDetails();
                    break;
                case 3:
                    ListAllAppointments();
                    break;
                case 4:
                    BookAppointments();
                    break;
                case 5:
                    //ExitToLogin();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
            }
        }

        public void ListPatientDetails()
        {
            Console.Clear();
            base.PrintHeader("My Details");
            Console.WriteLine();
            Console.WriteLine($"{currentPatient.Name}'s Details");
            Console.WriteLine($"Patient ID: {currentPatient.Username}");
            Console.WriteLine($"Full Name: {currentPatient.Name}");
            Console.WriteLine($"Address: {currentPatient.Address}");
            Console.WriteLine($"Email: {currentPatient.Email}");
            Console.WriteLine($"Phone: {currentPatient.Phone}");

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void ListDoctorDetails()
        {
            Console.Clear();
            base.PrintHeader("My Doctor");
            Console.WriteLine();
            Console.WriteLine("Your doctor: ");
            Console.WriteLine();

            var doctor = ((Patient)currentPatient).Doctor;

            Console.WriteLine("Name            | Email Address   | Phone        | Address     ");
            Console.WriteLine($"{doctor.Name,-16}| {doctor.Email,-18}| {doctor.Phone,-14}| {doctor.Address}");

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void ListAllAppointments()
        {
            Console.Clear();
            base.PrintHeader("My Appointments");
            Console.WriteLine();

            Console.WriteLine($"Appointments for {currentPatient.Name}");


            if (currentPatient != null)
            {
                foreach (var appointment in currentPatient.Appointments)
                {
                    Console.WriteLine("Write the top line here");
                    Console.WriteLine($"{appointment.Doctor.Name}       | {appointment.Patient.Name}        | {appointment.Description}");
                }
            }
            else
            {
                // error check here 
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void BookAppointments()
        {
            Console.Clear();
            base.PrintHeader("Book Appointment");
            
            if (((Patient)currentPatient).Doctor == null)
            {
                Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with");

                foreach (var doctor in _context.Doctors)
                {
                    Console.WriteLine(""); //finish the rest 
                }
            }
        }
    }
}

