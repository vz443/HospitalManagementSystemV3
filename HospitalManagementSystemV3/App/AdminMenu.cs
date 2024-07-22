using HospitalManagementSystemV3.App.Interface;
using HospitalManagementSystemV3.App.Print;
using HospitalManagementSystemV3.App.Repository;
using HospitalManagementSystemV3.Database;
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
            _doctorRepository = new DoctorRepository(context);

            _patientRepository = new PatientRepository(context);

            _loggedInUser = loggedInUser;
        }

        private DoctorRepository _doctorRepository;

        private PatientRepository _patientRepository;

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
            Console.Clear();
            PrintText.PrintHeader("All Doctors");
            Console.WriteLine();
            PrintText.PrintDoctors(_doctorRepository.GetAll().ToArray()); 
        }

        public void CheckDoctorDetails()
        {
            Console.Clear();
            PrintText.PrintHeader("Doctor Details");
            Console.WriteLine();

            Console.WriteLine("Please enter the ID of the doctor who's detail you are checking. Or press n to return to menu");
            var userID  = Console.ReadLine();
            Console.WriteLine();
            var doctor = _doctorRepository.GetById(userID);
            PrintText.PrintDoctors(new List<Doctor> { doctor });
        }

        public void ListAllPatients()
        {
           Console.Clear();
           PrintText.PrintHeader("All Patients");
           Console.WriteLine();
           PrintText.PrintMultiplePatients(_patientRepository.GetAll().ToArray());

        }

        public void CheckPatientDetails()
        {
            Console.Clear();
            PrintText.PrintHeader("Patient Details");

            Console.WriteLine();
            Console.WriteLine("Please enter the ID of the patient who's details you are checking. Or press n to return to menu");
            var id = Console.ReadLine();

            _patientRepository.GetById(id);
        }

        public void AddDoctor()
        {

        }

        public void AddPatient()
        {

        }
    }
}
