using HospitalManagementSystemV3.App.Interface;
using HospitalManagementSystemV3.App.Print;
using HospitalManagementSystemV3.App.Repository;
using HospitalManagementSystemV3.Database;
using System.Text;

namespace HospitalManagementSystemV3.App
{
    class DoctorMenu : IMenu
    {
        public DoctorMenu(AppDbContext context, IUser loggedInUser)
        {
            Console.Clear();

            _doctorRepository = new DoctorRepository(context);

            currentDoctor = (Doctor)loggedInUser;
            testContext = context;
            Startup();
        }

        DoctorRepository _doctorRepository;

        AppDbContext testContext;
        Doctor currentDoctor;

        void Startup()
        {
            DisplayMainMenu();
        }

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
                    Logout();
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
            }    
        }

        public void ListDoctorDetails()
        {
            Console.Clear();
            PrintText.PrintHeader("My Details");
            Console.WriteLine("Name            | Email Address   | Phone        | Address");
            Console.WriteLine($"{currentDoctor.Name,-16}| {currentDoctor.Email,-18}| {currentDoctor.Phone,-14}| {currentDoctor.Address}"); //make this line up 

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void ListPatients()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Patients assigned to {currentDoctor.Name}: ");
            Console.WriteLine();

            Console.WriteLine("Patient            | Doctor   | Email Address        | Phone     | Address");
            foreach (var patient in ((Doctor)currentDoctor).Patients)
            {
                Console.WriteLine($"{patient.Name,-16}| {currentDoctor.Name,-18}| {patient.Email,-14}| {patient.Phone}|  {patient.Address}"); //make this line up 
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void CheckParticularPatient()
        {
            Console.Clear();
           PrintText.PrintHeader("Check Patient Details");
            Console.WriteLine();

            Console.Write("Enter the ID of the patient to check: ");
            var ID = Console.ReadLine();
            var patients = _doctorRepository.GetAllPatients();
            var patient = _doctorRepository.GetPatientById(ID);
            PrintText.Print(patient);

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void ListAppointmentsWithPatient()
        {
            Console.Clear();
            PrintText.PrintHeader("Appointments With");
            Console.WriteLine();

            Console.Write("Enter the ID of the patient you would like to view the appointments for: ");
            var patientUsername = Console.ReadLine();
            var patient = _doctorRepository.GetPatientById(patientUsername);

            PrintText.Print(patient);

            if (patient == null)
            {
                Console.WriteLine("No patient found with the provided username.");
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void ListAllAppointments()
        {
            Console.Clear();
            PrintText.PrintHeader("All Appointments");
            Console.WriteLine();

            PrintText.Print(_doctorRepository.GetAllAppointmentsForDoctor(currentDoctor).ToArray());

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void Logout()
        {
            //call base logout
        }
    }
}
