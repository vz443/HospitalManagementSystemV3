using HospitalManagementSystemV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemV3.App.Print
{
    public class PrintText
    {
        public static void PrintHeader(string title)
        {
            int width = 40;

            Console.Write("┌");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("─");
            }
            Console.WriteLine("┐");

            Console.Write("│");
            string header = "DOTNET Hospital Management System";
            int padding = (width - 2 - header.Length) / 2;
            Console.Write(new string(' ', padding));
            Console.Write(header);
            Console.Write(new string(' ', width - 2 - padding - header.Length));
            Console.WriteLine("│");

            Console.Write("│");
            Console.Write(new string('-', width - 2));
            Console.WriteLine("│");

            Console.Write("│");
            padding = (width - 2 - title.Length) / 2;
            Console.Write(new string(' ', padding));
            Console.Write(title);
            Console.Write(new string(' ', width - 2 - padding - title.Length));
            Console.WriteLine("│");

            Console.Write("│");
            Console.Write(new string(' ', width - 2));
            Console.WriteLine("│");

            Console.Write("└");
            for (int i = 0; i < width - 2; i++)
            {
                Console.Write("─");
            }
            Console.WriteLine("┘");
        }

        public static void Print<T>(params T[] items) // change what passed in to match doctor 
        {
            if (items == null || items.Length == 0) return;

            var itemType = typeof(T);

            if (itemType == typeof(Doctor))
            {
                PrintDoctors(items.Cast<Doctor>().ToArray());
            }
            else if (itemType == typeof(Appointment))
            {
                PrintAppointments(items.Cast<Appointment>().ToArray());
            }
            else if (itemType == typeof(Patient))
            {
                PrintPatients(items.Cast<Patient>().ToArray());
            }
        }


        private static void PrintDoctors(params Doctor[] doctors) // this is broken and needs to be fixed
        {
            const int numberWidth = 5;
            const int nameWidth = 16;
            const int emailWidth = 18;
            const int phoneWidth = 14;
            const int addressWidth = 20;

            string FormatField(string value, int width)
            {
                if (value.Length <= width) return value.PadRight(width);
                return string.Join("\n", Enumerable.Range(0, (value.Length + width - 1) / width)
                    .Select(i => value.Substring(i * width, Math.Min(width, value.Length - i * width)).PadRight(width)));
            }

            Console.WriteLine("No.  | Name             | Email Address     | Phone         | Address");
            Console.WriteLine("-------------------------------------------------------------");

            int number = 1;
            foreach (var doctor in doctors)
            {
                var numberLines = number.ToString().PadRight(numberWidth).Split('\n');
                var nameLines = FormatField(doctor.Name, nameWidth).Split('\n');
                var emailLines = FormatField(doctor.Email, emailWidth).Split('\n');
                var phoneLines = FormatField(doctor.Phone, phoneWidth).Split('\n');
                var addressLines = FormatField(doctor.Address, addressWidth).Split('\n');

                var maxLines = new[] { numberLines.Length, nameLines.Length, emailLines.Length, phoneLines.Length, addressLines.Length }.Max();

                for (int i = 0; i < maxLines; i++)
                {
                    var numberText = i < numberLines.Length ? numberLines[i] : new string(' ', numberWidth);
                    var name = i < nameLines.Length ? nameLines[i] : new string(' ', nameWidth);
                    var email = i < emailLines.Length ? emailLines[i] : new string(' ', emailWidth);
                    var phone = i < phoneLines.Length ? phoneLines[i] : new string(' ', phoneWidth);
                    var address = i < addressLines.Length ? addressLines[i] : new string(' ', addressWidth);

                    Console.WriteLine($"{numberText} | {name} | {email} | {phone} | {address}");
                }

                Console.WriteLine("-------------------------------------------------------------");
                number++;
            }
        }

        private static void PrintPatients(ICollection<Patient> patients)
        {
            const int numberWidth = 5;
            const int patientNameWidth = 16;
            const int doctorNameWidth = 16;
            const int emailWidth = 18;
            const int phoneWidth = 14;
            const int addressWidth = 20;

            string FormatField(string value, int width)
            {
                if (value.Length <= width) return value.PadRight(width);
                return string.Join("\n", Enumerable.Range(0, (value.Length + width - 1) / width)
                    .Select(i => value.Substring(i * width, Math.Min(width, value.Length - i * width)).PadRight(width)));
            }

            Console.WriteLine("No.  | Patient Name     | Doctor Name       | Email Address     | Phone         | Address");
            Console.WriteLine("---------------------------------------------------------------------------------------");

            int number = 1;
            foreach (var patient in patients)
            {
                var numberLines = number.ToString().PadRight(numberWidth).Split('\n');
                var patientNameLines = FormatField(patient.Name, patientNameWidth).Split('\n');
                var doctorNameLines = FormatField(patient.Doctor?.Name ?? "No Doctor", doctorNameWidth).Split('\n');
                var emailLines = FormatField(patient.Email, emailWidth).Split('\n');
                var phoneLines = FormatField(patient.Phone, phoneWidth).Split('\n');
                var addressLines = FormatField(patient.Address, addressWidth).Split('\n');

                var maxLines = new[] { numberLines.Length, patientNameLines.Length, doctorNameLines.Length, emailLines.Length, phoneLines.Length, addressLines.Length }.Max();

                for (int i = 0; i < maxLines; i++)
                {
                    var numberText = i < numberLines.Length ? numberLines[i] : new string(' ', numberWidth);
                    var patientName = i < patientNameLines.Length ? patientNameLines[i] : new string(' ', patientNameWidth);
                    var doctorName = i < doctorNameLines.Length ? doctorNameLines[i] : new string(' ', doctorNameWidth);
                    var email = i < emailLines.Length ? emailLines[i] : new string(' ', emailWidth);
                    var phone = i < phoneLines.Length ? phoneLines[i] : new string(' ', phoneWidth);
                    var address = i < addressLines.Length ? addressLines[i] : new string(' ', addressWidth);

                    Console.WriteLine($"{numberText} | {patientName} | {doctorName} | {email} | {phone} | {address}");
                }

                Console.WriteLine("---------------------------------------------------------------------------------------");
                number++;
            }
        }

        private static void PrintAppointments(ICollection<Appointment> appointments)
        {
            const int numberWidth = 4;
            const int doctorNameWidth = 15;
            const int patientNameWidth = 15;
            const int descriptionWidth = 29;

            string FormatField(string value, int width)
            {
                if (value.Length <= width) return value.PadRight(width);
                return string.Join("\n", Enumerable.Range(0, (value.Length + width - 1) / width)
                    .Select(i => value.Substring(i * width, Math.Min(width, value.Length - i * width)).PadRight(width)));
            }

            Console.WriteLine($"{"No".PadRight(numberWidth)} | {"Doctor Name".PadRight(doctorNameWidth)} | {"Patient Name".PadRight(patientNameWidth)} | {"Description".PadRight(descriptionWidth)}");
            Console.WriteLine(new string('-', numberWidth + doctorNameWidth + patientNameWidth + descriptionWidth + 3 * 3 + 1));

            int number = 1;

            foreach (var appointment in appointments)
            {
                var numberLines = number.ToString().PadRight(numberWidth).Split('\n');
                var doctorNameLines = FormatField(appointment.Doctor.Name, doctorNameWidth).Split('\n');
                var patientNameLines = FormatField(appointment.Patient.Name, patientNameWidth).Split('\n');
                var descriptionLines = FormatField(appointment.Description, descriptionWidth).Split('\n');

                var maxLines = new[] { numberLines.Length, doctorNameLines.Length, patientNameLines.Length, descriptionLines.Length }.Max();

                for (int i = 0; i < maxLines; i++)
                {
                    var numberText = i < numberLines.Length ? numberLines[i] : new string(' ', numberWidth);
                    var doctorName = i < doctorNameLines.Length ? doctorNameLines[i] : new string(' ', doctorNameWidth);
                    var patientName = i < patientNameLines.Length ? patientNameLines[i] : new string(' ', patientNameWidth);
                    var description = i < descriptionLines.Length ? descriptionLines[i] : new string(' ', descriptionWidth);

                    Console.WriteLine($"{numberText} | {doctorName} | {patientName} | {description}");
                }

                number++;
            }
        }

    }
}
