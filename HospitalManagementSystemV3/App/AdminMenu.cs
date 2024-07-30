using Azure.Identity;
using HospitalManagementSystemV3.App.Interface;
using HospitalManagementSystemV3.App.Print;
using HospitalManagementSystemV3.App.Repository;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using System.Text;

namespace HospitalManagementSystemV3.App
{
    class AdminMenu : IMenu
    {
        public AdminMenu(AppDbContext context, IUser loggedInUser, Login login)
        {
            _adminRepository = new AdminRepository(context);

            _loggedInUser = loggedInUser;

            _login = login;

            DisplayMainMenu();
        }

        private AdminRepository _adminRepository;

        private Login _login;

        private IUser _loggedInUser;

        // Display the main menu for the administrator
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
                    _login.Logout();
                    break;
                case 8:
                    Environment.Exit(0);
                    break;
            }
        }

        // List all doctors in the system
        public void ListAllDoctors()
        {
            Utils.CreateHeader("All Doctors");
            var doctors = _adminRepository.GetAllDoctors();
            PrintText.Print(doctors?.ToArray() ?? new Doctor[0]);

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        // Print specific details about a doctor
        public void CheckDoctorDetails()
        {
            Utils.CreateHeader("Doctor Details");

            Console.WriteLine("Please enter the ID of the doctor whose details you are checking. Or press r to return to the menu.");
            var userId = Console.ReadLine();

            while (true)
            {
                if (userId == "r")
                {
                    DisplayMainMenu();
                    return;
                }

                var doctor = _adminRepository.GetDoctorById(userId);

                if (doctor != null)
                {
                    PrintText.Print(doctor);
                    break;
                }
                else
                {
                    Console.WriteLine("Doctor not found. Please enter a valid ID or press r to return to the menu.");
                    userId = Console.ReadLine();
                }
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        // List all patients in the system
        public void ListAllPatients()
        {
            Utils.CreateHeader("All Patients");
            var patients = _adminRepository.GetAllPatients();
            PrintText.Print(patients?.ToArray() ?? new Patient[0]);

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        // Check details of a specific patient
        public void CheckPatientDetails()
        {
            Utils.CreateHeader("Patient Details");
            Console.WriteLine("Please enter the ID of the patient whose details you are checking. Or press n to return to menu");
            var id = Console.ReadLine();

            var patient = _adminRepository.GetPatientById(id);

            if (patient != null)
            {
                PrintText.Print(patient);
            }
            else
            {
                Console.WriteLine("Patient not found.");
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        // Add a new doctor to the system
        public void AddDoctor()
        {
            Utils.CreateHeader("Add Doctor");

            Console.Write("Username: ");
            var username = Console.ReadLine() ?? string.Empty;

            Console.Write("Password: ");
            var password = Console.ReadLine() ?? string.Empty;

            Console.Write("First Name: ");
            var firstName = Console.ReadLine() ?? string.Empty;

            Console.Write("Last Name: ");
            var lastName = Console.ReadLine() ?? string.Empty;

            var name = firstName + lastName;

            Console.Write("Email: ");
            var email = Console.ReadLine() ?? string.Empty;

            Console.Write("Phone: ");
            var phone = Console.ReadLine() ?? string.Empty;

            Console.Write("Street Number: ");
            var streetNumber = Console.ReadLine() ?? string.Empty;

            Console.Write("Street: ");
            var street = Console.ReadLine() ?? string.Empty;


            var address = (streetNumber ?? string.Empty) + (street ?? string.Empty);

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

        // Add a new patient to the system
        public void AddPatient()
        {
            Utils.CreateHeader("Add Patient");

            Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");
            Console.Write("Username: ");
            var username = Console.ReadLine() ?? string.Empty;
            Console.Write("Password: ");
            var password = Console.ReadLine() ?? string.Empty;
            Console.Write("First Name: ");
            var firstName = Console.ReadLine() ?? string.Empty;
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine() ?? string.Empty;
            var name = firstName + lastName;
            Console.Write("Email: ");
            var email = Console.ReadLine() ?? string.Empty;
            Console.Write("Phone: ");
            var phone = Console.ReadLine() ?? string.Empty;
            Console.Write("Street Number: ");
            var streetNumber = Console.ReadLine();
            Console.Write("Street: ");
            var streetName = Console.ReadLine();
            Console.Write("City: ");
            var city = Console.ReadLine();
            Console.Write("State: ");
            var state = Console.ReadLine();
            var address = (streetNumber ?? string.Empty) + (streetName ?? string.Empty) + (city ?? string.Empty) + (state ?? string.Empty);

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
