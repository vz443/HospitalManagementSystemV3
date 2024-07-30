using Azure.Identity;
using HospitalManagementSystemV3.App.Interface;
using HospitalManagementSystemV3.App.Print;
using HospitalManagementSystemV3.App.Repository;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using System.Text;

namespace HospitalManagementSystemV3.App
{
    class AdminMenu : PrintText,  IMenu
    {
        public AdminMenu(AppDbContext context, IUser loggedInUser)
        {
            _adminRepository = new AdminRepository(context);

            _loggedInUser = loggedInUser;

            DisplayMainMenu();
        }

        private AdminRepository _adminRepository;

        IUser _loggedInUser;

        public void DisplayMainMenu()
        {
            Console.Clear();
            PrintText.PrintHeader("Administrator Menu");

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
            Utils.CreateHeader("All Doctors");
            PrintText.Print(_adminRepository.GetAllDoctors().ToArray());

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void CheckDoctorDetails()
        {
            Utils.CreateHeader("Doctor Details");

            Console.WriteLine("Please enter the ID of the doctor who's detail you are checking. Or press n to return to menu");
            var userID  = Console.ReadLine();
            Console.WriteLine();
            PrintText.Print(_adminRepository.GetDoctorById(userID));

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void ListAllPatients()
        {
           Console.Clear();
           PrintText.PrintHeader("All Patients");
           Console.WriteLine();
           PrintText.Print(_adminRepository.GetAllPatients().ToArray());

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void CheckPatientDetails()
        {
            Console.Clear();
            PrintText.PrintHeader("Patient Details");

            Console.WriteLine();
            Console.WriteLine("Please enter the ID of the patient who's details you are checking. Or press n to return to menu");
            var id = Console.ReadLine();

            _adminRepository.GetPatientById(id);

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void AddDoctor()
        {
            Console.Clear();
            PrintText.PrintHeader("Add Doctor");
            Console.WriteLine();

            Console.Write("Username: ");
            var username = Console.ReadLine();

            Console.WriteLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();
            Console.WriteLine();
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();
            Console.WriteLine();
            var name = firstName + lastName;
            Console.Write("Email: ");
            Console.WriteLine();
            var email = Console.ReadLine();
            Console.Write("Phone: ");
            var phone = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Street Number");
            var streetNumber = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Street");
            var street = Console.ReadLine();
            Console.WriteLine();

            var address = streetNumber + street;

            var doctor = new Doctor
            {
                Id = Guid.NewGuid(),  // Generate a new unique ID for the doctor
                Name = name,
                Email = email,
                Phone = phone,
                Address = address,
                Username = username,
                Password = password
            };

            _adminRepository.AddDoctor(doctor);

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void AddPatient()
        {
            Console.Clear();
            PrintText.PrintHeader("Add Patient");
            Console.WriteLine();

            Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();
            var name = firstName + lastName;
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Phone: ");
            var phone = Console.ReadLine();
            Console.Write("Street Number: ");
            var streetNumber = Console.ReadLine();
            Console.Write("Street: ");
            var streetName = Console.ReadLine();
            Console.Write("City: ");
            var city = Console.ReadLine();
            Console.Write("State: ");
            var state = Console.ReadLine();
            var address = streetNumber + streetName + city + state;

            var patient = new Patient
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Phone = phone,
                Address = address,
                Username = username,
                Password = password
            };

            _adminRepository.AddPatient(patient);

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }
    }
}
