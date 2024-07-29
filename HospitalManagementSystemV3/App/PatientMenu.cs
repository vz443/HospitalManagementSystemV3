using HospitalManagementSystemV3.App.Interface;
using HospitalManagementSystemV3.App.Print;
using HospitalManagementSystemV3.App.Repository;
using HospitalManagementSystemV3.Database;
using HospitalManagementSystemV3.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HospitalManagementSystemV3.App
{
    class PatientMenu : IMenu
    {
        public PatientMenu(AppDbContext context, IUser loggedInUser)
        {
            Console.Clear();

            _patientRepository = new PatientRepository(context);

            currentPatient = loggedInUser;

            Startup();
        }

        PatientRepository _patientRepository;

        IUser currentPatient;

        public void Startup()
        {
            DisplayMainMenu();
        }

        public void DisplayMainMenu()
        {
            Console.Clear();
            PrintText.PrintHeader("Patient Menu");
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
            PrintText.PrintHeader("My Details");
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
            PrintText.PrintHeader("My Doctor");
            Console.WriteLine();
            Console.WriteLine("Your doctor: ");
            Console.WriteLine();

            var doctor = ((Patient)currentPatient).Doctor;

            PrintText.Print(doctor);

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }

        public void ListAllAppointments() // this is broken for some reason 
        {
            Console.Clear();
            PrintText.PrintHeader("My Appointments");
            Console.WriteLine();
            var patient = (Patient)currentPatient;
            if (currentPatient != null)
            {

                if (patient != null)
                {
                    Console.WriteLine($"Appointments for {patient.Name}");
                    Console.WriteLine();

                    PrintText.Print(_patientRepository.GetAllAppointmentsForPatient(patient).ToArray());
                }
                else
                {
                    Console.WriteLine("No appointments found for the current patient.");
                }
            }
            else
            {
                Console.WriteLine("Current patient information is not available.");
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }


        public void BookAppointments()
        {
            Console.Clear();
            PrintText.PrintHeader("Book Appointment");
            Console.WriteLine();

            var patient = (Patient)currentPatient;

            // If the patient is not registered with any doctor
            if (patient.Doctor == null)
            {
                Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with");
                var doctors = _patientRepository.GetAllDoctors().ToArray();
                PrintText.Print(doctors);

                Console.WriteLine();
                Console.WriteLine("Enter number to select doctor: ");
                var number = Console.ReadLine();

                if (int.TryParse(number, out int doctorIndex) && doctorIndex >= 1 && doctorIndex <= doctors.Length)
                {
                    var doctor = doctors[doctorIndex - 1];
                    patient.Doctor = doctor;
                    _patientRepository.Update(patient); // Use repository method to update patient
                    _patientRepository.SaveChanges(); // Save changes to the database

                    Console.WriteLine("You have been successfully registered with the selected doctor.");
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
            } // get description as it doesnt work if it is choosing doctor
            {
                Console.WriteLine($"You are booking an appointment with {patient.Doctor.Name}");
                Console.WriteLine("Description of the appointment:");
                var description = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(description))
                {
                    Console.WriteLine("Appointment description cannot be empty.");
                    return;
                }

                var appointment = new Appointment
                {
                    Doctor = patient.Doctor,
                    Patient = patient,
                    Description = description
                };

                _patientRepository.AddAppointment(appointment); // Use repository method to add appointment
                _patientRepository.SaveChanges(); // Save changes to the database

                Console.WriteLine("The appointment has been booked successfully.");
            }

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
            DisplayMainMenu();
        }
    }
}










// make sure all the menus work and then use repo for everything fill in rest of ther guidelines and then write the tests 
// figure out printing multiple of the doctors nad how to approach that with differnet funcitons and also create service and unitofowrk class for the repository 