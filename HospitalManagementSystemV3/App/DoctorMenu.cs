using HospitalManagementSystemV3.App.Interface;
using HospitalManagementSystemV3.App.Print;
using HospitalManagementSystemV3.App.Repository;
using HospitalManagementSystemV3.Database;
using System.Text;

namespace HospitalManagementSystemV3.App
{
    class DoctorMenu : IMenu
    {
        public DoctorMenu(AppDbContext context, IUser loggedInUser, Login login)
        {
            Console.Clear();

            _doctorRepository = new DoctorRepository(context);

            _currentDoctor = (Doctor)loggedInUser;

            _login = login;

            DisplayMainMenu();
        }

        private DoctorRepository _doctorRepository;

        private Login _login;

        private Doctor _currentDoctor;

        /// <summary>
        /// Displays the main menu for the doctor.
        /// </summary>
        public void DisplayMainMenu()
        {
            Console.Clear();
            PrintText.PrintHeader("Doctor Menu");
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
                    ListPatients();
                    break;
                case 3:
                    ListAllAppointments();
                    break;
                case 4:
                    CheckParticularPatient();
                    break;
                case 5:
                    ListAppointmentsWithPatient();
                    break;
                case 6:
                    _login.Logout();
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
            }
        }

        /// <summary>
        /// Lists the details of the current doctor.
        /// </summary>
        public void ListDoctorDetails()
        {
            Utils.CreateHeader("My Details");
            PrintText.Print(_currentDoctor);

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        /// <summary>
        /// Lists all patients assigned to the current doctor.
        /// </summary>
        public void ListPatients()
        {
            Utils.CreateHeader("List Current Patients");
            Console.WriteLine($"Patients assigned to {_currentDoctor.Name}: ");
            Console.WriteLine();

            PrintText.Print(_currentDoctor.Patients);

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        /// <summary>
        /// Checks details of a specific patient by their ID. Allows retry if patient ID is not found.
        /// </summary>
        public void CheckParticularPatient()
        {
            Utils.CreateHeader("Particular Patient Details");

            bool isValidId = false;
            while (!isValidId)
            {
                Console.Write("Enter the ID of the patient to check (or press 'r' to return to the main menu): ");
                var id = Console.ReadLine() ?? string.Empty;

                if (id.ToLower() == "r")
                {
                    DisplayMainMenu();
                    return;
                }

                var patient = _doctorRepository.GetPatientById(id);
                if (patient != null)
                {
                    PrintText.Print(patient);
                    isValidId = true;
                }
                else
                {
                    Console.WriteLine("No patient found with the provided ID. Please try again.");
                }
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        /// <summary>
        /// Lists all appointments with a specific patient by their ID. Allows retry if patient ID is not found.
        /// </summary>
        public void ListAppointmentsWithPatient()
        {
            Utils.CreateHeader("Appointments with Patient");

            bool isValidId = false;
            while (!isValidId)
            {
                Console.Write("Enter the ID of the patient you would like to view the appointments for (or press 'r' to return to the main menu): ");
                var patientUsername = Console.ReadLine() ?? string.Empty;

                if (patientUsername.ToLower() == "r")
                {
                    DisplayMainMenu();
                    return;
                }

                var patient = _doctorRepository.GetPatientById(patientUsername);

                if (patient != null)
                {
                    PrintText.Print(patient);
                    isValidId = true;
                }
                else
                {
                    Console.WriteLine("No patient found with the provided username. Please try again.");
                }
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }


        /// <summary>
        /// Lists all appointments for the current doctor.
        /// </summary>
        public void ListAllAppointments()
        {
            Console.Clear();
            PrintText.PrintHeader("All Appointments");
            Console.WriteLine();

            PrintText.Print(_doctorRepository.GetAllAppointmentsForDoctor(_currentDoctor).ToArray());

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }
    }
}
